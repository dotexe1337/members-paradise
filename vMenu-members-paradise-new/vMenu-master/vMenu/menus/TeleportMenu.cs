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
    public class TeleportMenu
    {

        // Variables
        private Menu menu;

        /// <summary>
        /// Creates the menu.
        /// </summary>
        private void CreateMenu()
        {
            menu = new Menu(" ", "Teleport Options");
            menu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

            if (IsAllowed(Permission.MSTeleportToWp) || IsAllowed(Permission.MSTeleportLocations) || IsAllowed(Permission.MSTeleportToCoord))
            {

                MenuItem tptowp = new MenuItem("Teleport To Waypoint", "Teleport to the waypoint on your map.");
                MenuItem tpToCoord = new MenuItem("Teleport To Coords", "Enter x, y, z coordinates and you will be teleported to that location.");
                menu.OnItemSelect += async (sender, item, index) =>
                {
                    // Teleport to waypoint.
                    if (item == tptowp)
                    {
                        TeleportToWp();
                    }
                    else if (item == tpToCoord)
                    {
                        string x = await GetUserInput("Enter X coordinate.");
                        if (string.IsNullOrEmpty(x))
                        {
                            Notify.Error(CommonErrors.InvalidInput);
                            return;
                        }
                        string y = await GetUserInput("Enter Y coordinate.");
                        if (string.IsNullOrEmpty(y))
                        {
                            Notify.Error(CommonErrors.InvalidInput);
                            return;
                        }
                        string z = await GetUserInput("Enter Z coordinate.");
                        if (string.IsNullOrEmpty(z))
                        {
                            Notify.Error(CommonErrors.InvalidInput);
                            return;
                        }

                        float posX = 0f;
                        float posY = 0f;
                        float posZ = 0f;

                        if (!float.TryParse(x, out posX))
                        {
                            if (int.TryParse(x, out int intX))
                            {
                                posX = (float)intX;
                            }
                            else
                            {
                                Notify.Error("You did not enter a valid X coordinate.");
                                return;
                            }
                        }
                        if (!float.TryParse(y, out posY))
                        {
                            if (int.TryParse(y, out int intY))
                            {
                                posY = (float)intY;
                            }
                            else
                            {
                                Notify.Error("You did not enter a valid Y coordinate.");
                                return;
                            }
                        }
                        if (!float.TryParse(z, out posZ))
                        {
                            if (int.TryParse(z, out int intZ))
                            {
                                posZ = (float)intZ;
                            }
                            else
                            {
                                Notify.Error("You did not enter a valid Z coordinate.");
                                return;
                            }
                        }

                        await TeleportToCoords(new Vector3(posX, posY, posZ), true);
                    }
                };

                if (IsAllowed(Permission.MSTeleportToWp))
                {
                    menu.AddMenuItem(tptowp);
                }
                if (IsAllowed(Permission.MSTeleportToCoord))
                {
                    menu.AddMenuItem(tpToCoord);
                }

                // los santos submenu
                Menu losSantosMenu = new Menu(" ", "Los Santos");
                losSantosMenu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");
                MenuItem losSantosMenuBtn = new MenuItem("Los Santos", "Teleport to locations in Los Santos.");
                MenuController.AddSubmenu(menu, losSantosMenu);
                MenuController.BindMenuItem(menu, losSantosMenu, losSantosMenuBtn);
                losSantosMenuBtn.Label = "→→→";
                menu.AddMenuItem(losSantosMenuBtn);

                MenuItem airportBtn = new MenuItem("Airport", "Teleport to the airport.");
                losSantosMenu.AddMenuItem(airportBtn);
                MenuItem docksBtn = new MenuItem("Docks", "Teleport to the docks.");
                losSantosMenu.AddMenuItem(docksBtn);
                MenuItem obsBtn = new MenuItem("Observatory", "Teleport to the observatory.");
                losSantosMenu.AddMenuItem(obsBtn);

                losSantosMenu.OnItemSelect += async (sender, item, index) =>
                {
                    var entity = -1;
                    if(IsPedInAnyVehicle(PlayerPedId(), false))
                    {
                        entity = GetVehiclePedIsIn(PlayerPedId(), false);
                    }else
                    {
                        entity = PlayerPedId();
                    }
                    if (item == docksBtn)
                    {
                        await TeleportToCoords(new Vector3(978.33f, -3153.39f, 5.38f), true);
                        SetEntityHeading(entity, 360f);
                    }
                    if(item == airportBtn)
                    {
                        await TeleportToCoords(new Vector3(-1010.98f, -3138.41f, 13.41f), true);
                        SetEntityHeading(entity, 60f);
                    }
                    if(item == obsBtn)
                    {
                        await TeleportToCoords(new Vector3(-416.82f, 1215.24f, 325.11f), true);
                        SetEntityHeading(entity, 51.87f);
                    }
                };
            }
        }

        /// <summary>
        /// Create the menu if it doesn't exist, and then returns it.
        /// </summary>
        /// <returns>The Menu</returns>
        public Menu GetMenu()
        {
            if (menu == null)
            {
                CreateMenu();
            }
            return menu;
        }
    }
}

