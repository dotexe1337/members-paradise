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
    public class About
    {
        // Variables
        private Menu menu;

        private void CreateMenu()
        {
            // Create the menu.
            menu = new Menu("Members Paradise", "About Members Paradise");

            // Create menu items.
            MenuItem version = new MenuItem("vMenu-MP Version", $"This server is using vMenu-MP ~b~~h~{MainMenu.Version}~h~~s~.")
            {
                Label = $"~h~{MainMenu.Version}~h~"
            };
            MenuItem credits = new MenuItem("Credits", "vMenu was originally made by ~b~Vespura~s~. This version has been forked by ~b~dotexe~s~ for Members Paradise. Thanks to the staff & dev team for their amazing work.");

            menu.AddMenuItem(version);
            menu.AddMenuItem(credits);
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
