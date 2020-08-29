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
    public class VehicleSpawner
    {
        #region Variables
        Menu menu;
        public static Dictionary<string, uint> AddonVehicles;
        public bool SpawnInVehicle { get; private set; } = true;
        public bool ReplaceVehicle { get; private set; } = true;
        public bool TurnOffRadio { get; private set; } = true;
        public static List<bool> allowedCategories;
        public bool CanSpawn = true;
        readonly Player currentPlayer = new Player(Game.Player.Handle);
        public static string jsonData = LoadResourceFile(GetCurrentResourceName(), "config/cars.json") ?? "{}";
        public TheCarData array = JsonConvert.DeserializeObject<TheCarData>(jsonData);

        #endregion

        void CreateMenu()
        {
            #region initial setup.
            // Create the menu.
            menu = new Menu(" ", "Vehicle Spawner");
            menu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

            // Create the buttons and checkboxes.
            var spawnByName = new MenuItem("Spawn Vehicle By Model Name", "Enter the name of a vehicle to spawn.");

            // Add the items to the menu.
            if (IsAllowed(Permission.VSSpawnByName))
            {
                menu.AddMenuItem(spawnByName);
            }

            #endregion

            #region New Json Method
            foreach(var item in array.brands)
            {
                Console.WriteLine(item.brandName);
                // Get the class name.
                string className = item.brandName;

                // Create a button & a menu for it, add the menu to the menu pool and add & bind the button to the menu.
                var btn = new MenuItem(className, $"Spawn a vehicle from the ~f~{className} ~s~class.")
                {
                    Label = "→→→"
                };

                var vehicleClassMenu = new Menu(" ", className);
                vehicleClassMenu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");
                MenuController.AddSubmenu(menu, vehicleClassMenu);
                MenuController.BindMenuItem(menu, vehicleClassMenu, btn);

                if (item.staff == true)
                {
                    if (IsAllowed(Permission.VSStaff))
                    {
                        menu.AddMenuItem(btn);
                        btn.LeftIcon = MenuItem.Icon.NONE;
                        btn.Description = "Spawn a ~f~Staff ~s~car.";
                        btn.Enabled = true;
                    }
                }
                else
                {
                    menu.AddMenuItem(btn);
                }

                foreach (var veh in item.vehicles)
                {
                    // Convert the model name to start with a Capital letter, converting the other characters to lowercase. 
                    string properCasedModelName = veh[0].ToString().ToUpper() + veh.ToLower().Substring(1);

                    // Get the localized vehicle name, if it's "NULL" (no label found) then use the "properCasedModelName" created above.
                    string vehName = GetVehDisplayNameFromModel(veh) != "NULL" ? GetVehDisplayNameFromModel(veh) : properCasedModelName;
                    string vehModelName = veh;

                    if (DoesModelExist(veh))
                    {
                        var vehBtn = new MenuItem(vehName) { Enabled = true, Label = $"({vehModelName})" };
                        vehicleClassMenu.AddMenuItem(vehBtn);
                    }
                    else
                    {
                        var vehBtn = new MenuItem(vehName, "This vehicle is not available because the model could not be found in your game files. If this is a DLC vehicle, make sure the server is streaming it.") { Enabled = false, Label = $"({vehModelName.ToLower()})" };
                        vehicleClassMenu.AddMenuItem(vehBtn);
                        vehBtn.RightIcon = MenuItem.Icon.LOCK;
                    }
                }

                // Handle button presses
                vehicleClassMenu.OnItemSelect += async (sender2, item2, index2) =>
                {
                    if (IsAllowed(Permission.VSSpawnByName))
                    {
                        SpawnVehicle(item.vehicles[index2], SpawnInVehicle, ReplaceVehicle);
                    }
                    else
                    {
                        if (CanSpawn)
                        {
                            SpawnVehicle(item.vehicles[index2], SpawnInVehicle, ReplaceVehicle);
                            CanSpawn = false;
                            await Delay(6000);
                            CanSpawn = true;
                            int tmpTimer = GetGameTimer();
                            while (GetGameTimer() - tmpTimer < 6000) // wait 30 _real_ seconds
                            {
                                await Delay(0);
                                float carCoolDownState = (GetGameTimer() - (float)tmpTimer) / 6000f;
                                foreach (var it in vehicleClassMenu.GetMenuItems())
                                {
                                    if (!it.Text.Contains("Donator") && !it.Text.Contains("Fast"))
                                    {
                                        it.Enabled = false;
                                        it.Label = $"Cooldown: {Math.Ceiling(6f - (6f * carCoolDownState))}";
                                    }
                                }

                                foreach (var i in menu.GetMenuItems())
                                {
                                    if (!i.Text.Contains("Donator") && !i.Text.Contains("Fast"))
                                    {
                                        i.Enabled = false;
                                        i.Label = $"Cooldown: {Math.Ceiling(6f - (6f * carCoolDownState))}";
                                    }
                                }
                            }

                            foreach (var it in vehicleClassMenu.GetMenuItems())
                            {
                                if (!it.Text.Contains("Donator") && !it.Text.Contains("Fast"))
                                {
                                    it.Enabled = true;
                                    it.Label = "→→→";
                                }
                            }

                            foreach (var i in menu.GetMenuItems())
                            {
                                if (!i.Text.Contains("Donator") && !i.Text.Contains("Fast"))
                                {
                                    i.Enabled = true;
                                    i.Label = "→→→";
                                }
                            }
                        }
                        else
                        {
                            Notify.Info("Wait for the 6 second cooldown before spawning another car");
                        }
                        // MainMenu.VehicleSpawnerMenu.GetMenu().CounterPreText = null;
                    }
                };
            }
            #endregion

            #region handle events
            // Handle button presses.
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == spawnByName)
                {
                    // Passing "custom" as the vehicle name, will ask the user for input.
                    SpawnVehicle("custom", SpawnInVehicle, ReplaceVehicle);
                }
            };
            #endregion
        }

        #region Just the struct for the vehicle classes json
        public class TheCarData
        {
            [JsonProperty("brands")]
            public List<TheCars> brands { get; set; }
        }

        public class TheCars
        {
            [JsonProperty("brandName")]
            public string brandName { get; set; }
            [JsonProperty("staff")]
            public bool staff { get; set; }
            [JsonProperty("vehicles")]
            public List<string> vehicles { get; set; }
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
    }
}
