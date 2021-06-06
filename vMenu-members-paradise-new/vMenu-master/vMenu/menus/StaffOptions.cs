using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static vMenuClient.CommonFunctions;
using static vMenuShared.PermissionsManager;

namespace vMenuClient
{
    public class StaffOptions
    {

        // Variables
        private Menu menu;

        /// <summary>
        /// Creates the menu.
        /// </summary>
        private void CreateMenu()
        {
            menu = new Menu(" ", "Staff Options");
            menu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

            MenuItem broadcastBtn = new MenuItem("Broadcast Message");
            menu.AddMenuItem(broadcastBtn);

            menu.OnItemSelect += async (sender, item, index) =>
            {
                if(item == broadcastBtn)
                {
                    SendMessage();
                }
            };
        }

        public static async void SendMessage()
        {
            String text = await GetUserInput(windowTitle: "Enter message to broadcast", defaultText: "", maxInputLength: 1337);
            if (!string.IsNullOrEmpty(text))
            {
                BroadcastMessage(text);
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
