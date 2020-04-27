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
    public class TimeOptions
    {
        // Variables
        private Menu menu;

        public MenuCheckboxItem clientSidedTimeEnabled;
        public MenuCheckboxItem timeFrozen;
        public MenuListItem timeDataList;

        /// <summary>
        /// Creates the menu.
        /// </summary>
        private void CreateMenu()
        {
            menu = new Menu(Game.Player.Name, "Time Options");

            clientSidedTimeEnabled = new MenuCheckboxItem("Client-Sided Time", "Enable or disable client-sided time changes.", false);
            menu.AddMenuItem(clientSidedTimeEnabled);

            timeFrozen = new MenuCheckboxItem("Freeze Time", "Enable or disable time freezing.", false);
            menu.AddMenuItem(timeFrozen);

            List<string> timeData = new List<string>();
            for (var i = 0; i < 24; i++)
            {
                timeData.Add(i.ToString() + ".00");
            }
            timeDataList = new MenuListItem("Change Time", timeData, 12, "Select time of day.");
            menu.AddMenuItem(timeDataList);
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
