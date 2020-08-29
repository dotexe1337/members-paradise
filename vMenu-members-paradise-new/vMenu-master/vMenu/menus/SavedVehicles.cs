using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using Newtonsoft.Json;
using CitizenFX.Core;
using static CitizenFX.Core.UI.Screen;
using static CitizenFX.Core.Native.API;
using static vMenuClient.CommonFunctions;
using static vMenuShared.PermissionsManager;

namespace vMenuClient
{
    public class SavedVehicles : BaseScript
    {
        // Variables
        Menu menu;
        readonly Menu selectedVehicleMenu = new Menu(" ", "Manage this saved vehicle.");
        Dictionary<string, VehicleInfo> savedVehicles = new Dictionary<string, VehicleInfo>();
        readonly List<Menu> subMenus = new List<Menu>();
        Dictionary<MenuItem, KeyValuePair<string, VehicleInfo>> svMenuItems = new Dictionary<MenuItem, KeyValuePair<string, VehicleInfo>>();
        KeyValuePair<string, VehicleInfo> currentlySelectedVehicle = new KeyValuePair<string, VehicleInfo>();
        int deleteButtonPressedCount;
        public bool CanSpawn = true;

        public static string jsonData = LoadResourceFile(GetCurrentResourceName(), "config/cars.json") ?? "{}";
        public TheCarData array = JsonConvert.DeserializeObject<TheCarData>(jsonData);

        /// <summary>
        /// Creates the menu.
        /// </summary>
        void CreateMenu()
        {
            #region Create menus and submenus
            // Create the menu.
            menu = new Menu(" ", "Manage Cars");
            menu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

            var saveVehicle = new MenuItem("Save Current Vehicle", "Save the vehicle you are currently sitting in.");
            menu.AddMenuItem(saveVehicle);
            saveVehicle.RightIcon = MenuItem.Icon.CAR;

            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == saveVehicle)
                {
                    if (Game.PlayerPed.IsInVehicle())
                    {
                        SaveVehicle();
                    }
                    else
                    {
                        Notify.Error("You are currently not in any vehicle. Please enter a vehicle before trying to save it.");
                    }
                }
            };

            for (int i = 0; i < array.brands.Count(); i++)
            {
                var categoryName = array.brands[i].brandName;

                var categoryMenu = new Menu(" ", categoryName);
                categoryMenu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

                var categoryButton = new MenuItem(categoryName, $"All saved vehicles from the {categoryName} category.");

                subMenus.Add(categoryMenu);
                MenuController.AddSubmenu(menu, categoryMenu);
                menu.AddMenuItem(categoryButton);
                categoryButton.Label = "→→→";
                MenuController.BindMenuItem(menu, categoryMenu, categoryButton);

                categoryMenu.OnMenuClose += (sender) =>
                {
                    UpdateMenuAvailableCategories();
                };

                categoryMenu.OnItemSelect += (sender, item, index) =>
                {
                    UpdateSelectedVehicleMenu(item, sender);
                };
            }

            selectedVehicleMenu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

            var unavailableModels = new MenuItem("Unavailable Saved Vehicles", "These vehicles are currently unavailable because the models are not present in the game. These vehicles are most likely not being streamed from the server.")
            {
                Label = "→→→"
            };

            MenuController.AddMenu(selectedVehicleMenu);
            var spawnVehicle = new MenuItem("Spawn Vehicle", "Spawn this saved vehicle.");
            var renameVehicle = new MenuItem("Rename Vehicle", "Rename your saved vehicle.");
            var replaceVehicle = new MenuItem("~r~Replace Vehicle", "Your saved vehicle will be replaced with the vehicle you are currently sitting in. ~r~Warning: this can NOT be undone!");
            var deleteVehicle = new MenuItem("~r~Delete Vehicle", "~r~This will delete your saved vehicle. Warning: this can NOT be undone!");
            selectedVehicleMenu.AddMenuItem(spawnVehicle);
            selectedVehicleMenu.AddMenuItem(renameVehicle);
            selectedVehicleMenu.AddMenuItem(replaceVehicle);
            selectedVehicleMenu.AddMenuItem(deleteVehicle);

            selectedVehicleMenu.OnMenuOpen += (sender) =>
            {
                spawnVehicle.Label = "(" + GetDisplayNameFromVehicleModel(currentlySelectedVehicle.Value.model).ToLower() + ")";
            };

            selectedVehicleMenu.OnMenuClose += (sender) =>
            {
                selectedVehicleMenu.RefreshIndex();
                deleteButtonPressedCount = 0;
                deleteVehicle.Label = "";
            };

            selectedVehicleMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == spawnVehicle)
                {
                    if (MainMenu.VehicleSpawnerMenu != null)
                    {
                        if (IsAllowed(Permission.VSSpawnByName))
                        {
                            SpawnVehicle(currentlySelectedVehicle.Value.model, MainMenu.VehicleSpawnerMenu.SpawnInVehicle, MainMenu.VehicleSpawnerMenu.ReplaceVehicle, false, vehicleInfo: currentlySelectedVehicle.Value, saveName: currentlySelectedVehicle.Key.Substring(4));
                        }
                        else
                        {
                            if (CanSpawn)
                            {
                                SpawnVehicle(currentlySelectedVehicle.Value.model, MainMenu.VehicleSpawnerMenu.SpawnInVehicle, MainMenu.VehicleSpawnerMenu.ReplaceVehicle, false, vehicleInfo: currentlySelectedVehicle.Value, saveName: currentlySelectedVehicle.Key.Substring(4));
                                Spawned();
                                int tmpTimer = GetGameTimer();
                                while (GetGameTimer() - tmpTimer < 6000) // wait 30 _real_ seconds
                                {
                                    await Delay(0);
                                    float carCoolDownState = (GetGameTimer() - (float)tmpTimer) / 6000f;
                                    spawnVehicle.Enabled = false;
                                    spawnVehicle.Description = $"Cooldown: {Math.Ceiling(6f - (6f * carCoolDownState))}";
                                }

                                spawnVehicle.Enabled = true;
                                spawnVehicle.Description = "Spawn this saved vehicle.";
                            }
                            else
                            {
                                Notify.Info("Wait for the 6 second cooldown before spawning another car");
                            }

                            MainMenu.SavedVehiclesMenu.GetMenu().CounterPreText = null;
                        }
                    }
                    else
                    {
                        SpawnVehicle(currentlySelectedVehicle.Value.model, true, true, false, vehicleInfo: currentlySelectedVehicle.Value, saveName: currentlySelectedVehicle.Key.Substring(4));
                    }
                }
                else if (item == renameVehicle)
                {
                    var newName = await GetUserInput(windowTitle: "Enter a new name for this vehicle.", maxInputLength: 30);
                    if (string.IsNullOrEmpty(newName))
                    {
                        Notify.Error(CommonErrors.InvalidInput);
                    }
                    else
                    {
                        if (StorageManager.SaveVehicleInfo("veh_" + newName, currentlySelectedVehicle.Value, false))
                        {
                            DeleteResourceKvp(currentlySelectedVehicle.Key);
                            while (!selectedVehicleMenu.Visible)
                                await Delay(0);

                            Notify.Success("Your vehicle has successfully been renamed.");
                            UpdateMenuAvailableCategories();
                            selectedVehicleMenu.GoBack();
                            currentlySelectedVehicle = new KeyValuePair<string, VehicleInfo>(); // clear the old info
                        }
                        else
                        {
                            Notify.Error("This name is already in use or something unknown failed. Contact the server owner if you believe something is wrong.");
                        }
                    }
                }
                else if (item == replaceVehicle)
                {
                    if (Game.PlayerPed.IsInVehicle())
                    {
                        SaveVehicle(currentlySelectedVehicle.Key.Substring(4));
                        selectedVehicleMenu.GoBack();
                        Notify.Success("Your saved vehicle has been replaced with your current vehicle.");
                    }
                    else
                    {
                        Notify.Error("You need to be in a vehicle before you can relplace your old vehicle.");
                    }
                }
                else if (item == deleteVehicle)
                {
                    if (deleteButtonPressedCount == 0)
                    {
                        deleteButtonPressedCount = 1;
                        item.Label = "Press again to confirm.";
                        Notify.Alert("Are you sure you want to delete this vehicle? Press the button again to confirm.");
                    }
                    else
                    {
                        deleteButtonPressedCount = 0;
                        item.Label = "";
                        DeleteResourceKvp(currentlySelectedVehicle.Key);
                        UpdateMenuAvailableCategories();
                        selectedVehicleMenu.GoBack();
                        Notify.Success("Your saved vehicle has been deleted.");
                    }
                }

                if (item != deleteVehicle) // if any other button is pressed, restore the delete vehicle button pressed count.
                {
                    deleteButtonPressedCount = 0;
                    deleteVehicle.Label = "";
                }
            };

            #endregion
        }

        /// <summary>
        /// Updates the selected vehicle.
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns>A bool, true if successfull, false if unsuccessfull</returns>
        bool UpdateSelectedVehicleMenu(MenuItem selectedItem, Menu parentMenu = null)
        {
            if (!svMenuItems.ContainsKey(selectedItem))
            {
                Notify.Error("In some very strange way, you've managed to select a button, that does not exist according to this list. So your vehicle could not be loaded. :( Maybe your save files are broken?");
                return false;
            }

            var vehInfo = svMenuItems[selectedItem];
            selectedVehicleMenu.MenuSubtitle = $"{vehInfo.Key.Substring(4)} ({vehInfo.Value.name})";
            currentlySelectedVehicle = vehInfo;
            MenuController.CloseAllMenus();
            selectedVehicleMenu.OpenMenu();
            if (parentMenu != null)
            {
                MenuController.AddSubmenu(parentMenu, selectedVehicleMenu);
            }

            return true;
        }




        /// <summary>
        /// Updates the available vehicle category list.
        /// </summary>
        public void UpdateMenuAvailableCategories()
        {
            // PATCHED BY DOTEXE FOR CUSTOM CATEGORIES

            // Check if the items count will be changed. If there are less cars than there were before, one probably got deleted
            // so in that case we need to refresh the index of that menu just to be safe. If not, keep the index where it is for improved
            // usability of the menu.
            foreach (Menu m in subMenus)
            {
                int size = m.Size;
                int vclass = subMenus.IndexOf(m);

                int count = savedVehicles.Count(a => GetVehicleClassFromName(a.Value.model) == vclass);
                if (count < size)
                {
                    m.RefreshIndex();
                }
            }

            foreach (Menu m in subMenus)
            {
                // Clear items but don't reset the index because we can guarantee that the index won't be out of bounds.
                // this is the case because of the loop above where we reset the index if the items count changes.
                m.ClearMenuItems(true);
            }


            savedVehicles = GetSavedVehicles();
            svMenuItems = new Dictionary<MenuItem, KeyValuePair<string, VehicleInfo>>();

            for (int i = 1; i < GetMenu().Size; i++)
            {
                GetMenu().GetMenuItems()[i].RightIcon = MenuItem.Icon.NONE;
                GetMenu().GetMenuItems()[i].Label = "→→→";
                GetMenu().GetMenuItems()[i].Enabled = true;
                GetMenu().GetMenuItems()[i].Description = $"All saved vehicles from the {GetMenu().GetMenuItems()[i].Text} category.";
            }

            for (int i = 0; i < subMenus.Count(); i++)
            {
                foreach (var item in array.brands)
                {
                    if (item == array.brands.ElementAt(i))
                    {
                        foreach (string veh in item.vehicles)
                        {
                            foreach (KeyValuePair<string, VehicleInfo> dict in savedVehicles)
                            {
                                if (veh == GetDisplayNameFromVehicleModel(dict.Value.model) && IsModelInCdimage(dict.Value.model))
                                {
                                    Menu menu = subMenus[i];

                                    MenuItem savedVehicleBtn = new MenuItem(dict.Key.Substring(4), $"Manage this saved vehicle.")
                                    {
                                        Label = $"({dict.Value.name}) →→→"
                                    };
                                    menu.AddMenuItem(savedVehicleBtn);

                                    svMenuItems.Add(savedVehicleBtn, dict);
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < subMenus.Count(); i++)
            {
                Menu menu = subMenus[i];
                if (menu.GetMenuItems().Count() == 0)
                {
                    GetMenu().GetMenuItems()[i + 1].Label = "";
                    GetMenu().GetMenuItems()[i + 1].RightIcon = MenuItem.Icon.LOCK;
                    GetMenu().GetMenuItems()[i + 1].Enabled = false;
                    GetMenu().GetMenuItems()[i + 1].Description = $"You do not have any saved vehicles that belong to the {GetMenu().GetMenuItems()[i + 1].Text} category.";
                }
            }
        }

        #region Just the struct for the vehicle classes json
        public class TheCarData
        {
            [JsonProperty("brands")]
            public List<TheCars> brands = new List<TheCars>();
        }

        public class TheCars
        {
            [JsonProperty("brandName")]
            public string brandName = "";
            [JsonProperty("staff")]
            public bool staff = false;
            [JsonProperty("vehicles")]
            public List<string> vehicles = new List<string>();
        }
        #endregion

        /// <summary>
        /// Create the menu if it doesn't exist, and then returns it.
        /// </summary>
        /// <returns>The Menu</returns>
        public Menu GetMenu()
        {
            if (menu == null) CreateMenu();

            return menu;
        }

        async void Spawned()
        {
            CanSpawn = false;
            await Delay(6000);
            CanSpawn = true;
        }
    }
}
