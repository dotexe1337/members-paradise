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
    public class PlayerTimeWeatherOptions
    {

        // Variables
        private Menu menu;

        public MenuCheckboxItem clientSidedEnabled;
        public MenuCheckboxItem timeFrozen;
        public MenuListItem timeDataList;
        public static Dictionary<uint, MenuItem> weatherHashMenuIndex = new Dictionary<uint, MenuItem>();
        public List<string> weatherListData = new List<string>() { "Clear", "Extrasunny", "Clouds", "Overcast", "Rain", "Clearing", "Thunder", "Smog", "Foggy", "Xmas", "Snowlight", "Blizzard", "Snow", "Halloween", "Neutral" };
        public MenuListItem weatherList;

        /// <summary>
        /// Creates the menu.
        /// </summary>
        private void CreateMenu()
        {
            menu = new Menu(" ", "Time & Weather Options");
            menu.HeaderTexture = new KeyValuePair<string, string>("mp_header", "mp_header");

            clientSidedEnabled = new MenuCheckboxItem("Client-Sided Time & Weather", "Enable or disable client-sided time and weather changes.", false);
            menu.AddMenuItem(clientSidedEnabled);

            timeFrozen = new MenuCheckboxItem("Freeze Time", "Enable or disable time freezing.", false);
            menu.AddMenuItem(timeFrozen);

            List<string> timeData = new List<string>();
            for (var i = 0; i < 24; i++)
            {
                timeData.Add(i.ToString() + ".00");
            }
            timeDataList = new MenuListItem("Change Time", timeData, 12, "Select time of day.");
            menu.AddMenuItem(timeDataList);

            weatherList = new MenuListItem("Change Weather", weatherListData, 0, "Select weather.");
            menu.AddMenuItem(weatherList);
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

