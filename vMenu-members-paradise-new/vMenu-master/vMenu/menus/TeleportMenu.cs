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
                Menu customMapsMenu = new Menu(" ", "Custom Maps");
                customMapsMenu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");
                MenuItem customMapsMenuBtn = new MenuItem("Custom Maps", "Teleport to custom map locations..");
                MenuController.AddSubmenu(menu, customMapsMenu);
                MenuController.BindMenuItem(menu, customMapsMenu, customMapsMenuBtn);
                customMapsMenuBtn.Label = "→→→";
                menu.AddMenuItem(customMapsMenuBtn);

                MenuItem airportBtn = new MenuItem("Airport", "Teleport to the airport.");
                losSantosMenu.AddMenuItem(airportBtn);
                MenuItem docksBtn = new MenuItem("Docks", "Teleport to the docks.");
                losSantosMenu.AddMenuItem(docksBtn);
                MenuItem obsBtn = new MenuItem("Observatory", "Teleport to the observatory.");
                losSantosMenu.AddMenuItem(obsBtn);
                MenuItem galaxyBtn = new MenuItem("Galaxy Garage", "Teleport to the galaxy garage.");
                losSantosMenu.AddMenuItem(galaxyBtn);
                MenuItem malibuBtn = new MenuItem("Malibu Mansion", "Teleport to the malibu mansion.");
                losSantosMenu.AddMenuItem(malibuBtn);
                MenuItem starscreamersBtn = new MenuItem("Starscreamers Garage", "Teleport to the starscreamers garage.");
                losSantosMenu.AddMenuItem(starscreamersBtn);
                MenuItem bahamaBtn = new MenuItem("Bahama Mamas", "Teleport to the bahama mamas.");
                losSantosMenu.AddMenuItem(bahamaBtn);
                MenuItem parkingBtn = new MenuItem("Parking Lot", "Teleport to the parking lot.");
                losSantosMenu.AddMenuItem(parkingBtn);
                MenuItem mosleysBtn = new MenuItem("Mosleys Auto Shop", "Teleport to the mosleys auto shop.");
                losSantosMenu.AddMenuItem(mosleysBtn);
                MenuItem beachBtn = new MenuItem("Beach Bridge", "Teleport to the beach bridge.");
                losSantosMenu.AddMenuItem(beachBtn);
                MenuItem takimoBtn = new MenuItem("Takimo Garage", "Teleport to the takimo garage.");
                losSantosMenu.AddMenuItem(takimoBtn);
                MenuItem blaineBtn = new MenuItem("Blaine County Garage", "Teleport to the blaine county garage.");
                losSantosMenu.AddMenuItem(blaineBtn);
                MenuItem designerBtn = new MenuItem("Designer House", "Teleport to the designer house.");
                losSantosMenu.AddMenuItem(designerBtn);

                MenuItem shibuyaBtn = new MenuItem("Shibuya", "Teleport to shibuya.");
                customMapsMenu.AddMenuItem(shibuyaBtn);

                losSantosMenu.OnItemSelect += async (sender, item, index) =>
                {
                    var entity = -1;
                    if (IsPedInAnyVehicle(PlayerPedId(), false))
                    {
                        entity = GetVehiclePedIsIn(PlayerPedId(), false);
                    }
                    else
                    {
                        entity = PlayerPedId();
                    }
                    if (item == docksBtn)
                    {
                        await TeleportToCoords(new Vector3(978.33f, -3153.39f, 5.38f), true);
                        SetEntityHeading(entity, 360f);
                    }
                    if (item == airportBtn)
                    {
                        await TeleportToCoords(new Vector3(-1010.98f, -3138.41f, 13.41f), true);
                        SetEntityHeading(entity, 60f);
                    }
                    if (item == obsBtn)
                    {
                        await TeleportToCoords(new Vector3(-416.82f, 1215.24f, 325.11f), true);
                        SetEntityHeading(entity, 51.87f);
                    }
                    if (item == galaxyBtn)
                    {
                        await TeleportToCoords(new Vector3(-1158.63f, -1760.92f, 3.48f), true);
                        SetEntityHeading(entity, 35.25f);
                    }
                    if (item == malibuBtn)
                    {
                        await TeleportToCoords(new Vector3(-3117.18f, 804.7f, 17.38f), true);
                        SetEntityHeading(entity, 116.12f);
                    }
                    if (item == starscreamersBtn)
                    {
                        await TeleportToCoords(new Vector3(-1165.24f, -949.56f, 2.39f), true);
                        SetEntityHeading(entity, 302.43f);
                    }
                    if (item == bahamaBtn)
                    {
                        await TeleportToCoords(new Vector3(-1400.66f, -585.65f, 29.73f), true);
                        SetEntityHeading(entity, 296.15f);
                    }
                    if (item == parkingBtn)
                    {
                        await TeleportToCoords(new Vector3(198.28f, -814.79f, 30.47f), true);
                        SetEntityHeading(entity, 251.0f);
                    }
                    if (item == mosleysBtn)
                    {
                        await TeleportToCoords(new Vector3(-60.88f, -1668.33f, 29.21f), true);
                        SetEntityHeading(entity, 298.29f);
                    }
                    if (item == beachBtn)
                    {
                        await TeleportToCoords(new Vector3(-2060.19f, -450.48f, 11.42f), true);
                        SetEntityHeading(entity, 226.28f);
                    }
                    if (item == takimoBtn)
                    {
                        await TeleportToCoords(new Vector3(-226.07f, -2639.43f, 6.0f), true);
                        SetEntityHeading(entity, 178.58f);
                    }
                    if (item == blaineBtn)
                    {
                        await TeleportToCoords(new Vector3(-224.89f, 6260.23f, 31.47f), true);
                        SetEntityHeading(entity, 204.03f);
                    }
                    if (item == designerBtn)
                    {
                        await TeleportToCoords(new Vector3(-2595.31f, 1657.76f, 140.21f), true);
                        SetEntityHeading(entity, 112.43f);
                    }
                };

                customMapsMenu.OnItemSelect += async (sender, item, index) =>
                {
                    var entity = -1;
                    if (IsPedInAnyVehicle(PlayerPedId(), false))
                    {
                        entity = GetVehiclePedIsIn(PlayerPedId(), false);
                    }
                    else
                    {
                        entity = PlayerPedId();
                    }
                    if (item == shibuyaBtn)
                    {
                        await TeleportToCoords(new Vector3(-5852.93f, 738.42f, 26.05f), true);
                        SetEntityHeading(entity, 5.3f);
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

