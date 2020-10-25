using MenuAPI;
using System.Collections.Generic;

namespace vMenuClient
{
    public class About
    {
        // Variables
        private Menu menu;

        private void CreateMenu()
        {
            // Create the menu.
            menu = new Menu(" ", "About Members Paradise");
            menu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

            // Create menu items.
            MenuItem version = new MenuItem("vMenu-MP Version", $"This server is using vMenu-MP ~b~~h~{MainMenu.Version}~h~~s~.")
            {
                Label = $"~h~{MainMenu.Version}~h~"
            };
            MenuItem credits = new MenuItem("Credits", "vMenu was originally made by ~b~Vespura~s~. This version has been forked by ~b~dotexe~s~ for ~r~Members Paradise~s~. Thanks to the staff & dev team for their amazing work.");

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
