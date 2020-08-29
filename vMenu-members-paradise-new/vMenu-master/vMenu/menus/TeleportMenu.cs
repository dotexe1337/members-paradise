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
        Menu menu;
        readonly Player currentPlayer = new Player(Game.Player.Handle);
        public static string jsonData = LoadResourceFile(GetCurrentResourceName(), "config/teleports.json") ?? "{}";
        public TheTeleportData array = JsonConvert.DeserializeObject<TheTeleportData>(jsonData);

        void CreateMenu()
        {
            // Create the menu.
            menu = new Menu(" ", "Teleports");
            menu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

            foreach (var item in array.categories)
            {
                string categoryName = item.categoryName;

                var btn = new MenuItem(categoryName, $"Teleport to a location from ~f~{categoryName}~s~.")
                {
                    Label = "→→→"
                };

                var teleportCategoryMenu = new Menu(" ", categoryName);
                teleportCategoryMenu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");
                MenuController.AddSubmenu(menu, teleportCategoryMenu);
                MenuController.BindMenuItem(menu, teleportCategoryMenu, btn);

                menu.AddMenuItem(btn);

                foreach (var tele in item.teleports)
                {

                    // Get the localized vehicle name, if it's "NULL" (no label found) then use the "properCasedModelName" created above.
                    string teleName = tele.teleportName;
                    List<float> coords = tele.coords;

                    var teleBtn = new MenuItem(teleName) { Enabled = true };
                    teleportCategoryMenu.AddMenuItem(teleBtn);
                }

                teleportCategoryMenu.OnItemSelect += async (sender2, item2, index2) =>
                {
                    float x = item.teleports[index2].coords[0];
                    float y = item.teleports[index2].coords[1];
                    float z = item.teleports[index2].coords[2];
                    float h = item.teleports[index2].coords[3];
                    await TeleportToCoords(new Vector3(x, y, z), true);
                    SetEntityHeading(-1, h);
                };
            }
        }

        #region Just the struct for the teleports json
        public class TheTeleportData
        {
            [JsonProperty("categories")]
            public List<TheTeleports> categories { get; set; }
        }

        public class TheTeleports
        {
            [JsonProperty("categoryName")]
            public string categoryName { get; set; }
            [JsonProperty("teleports")]
            public List<ATeleport> teleports { get; set; }
        }

        public class ATeleport
        {
            [JsonProperty("teleportName")]
            public string teleportName { get; set; }
            [JsonProperty("coords")]
            public List<float> coords { get; set; }
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

