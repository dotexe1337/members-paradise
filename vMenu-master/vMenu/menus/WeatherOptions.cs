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
    public class WeatherOptions
    {
        // Variables
        private Menu menu;
        public static Dictionary<uint, MenuItem> weatherHashMenuIndex = new Dictionary<uint, MenuItem>();
        public MenuCheckboxItem clientSidedWeatherEnabled;
        public List<string> weatherListData = new List<string>() { "Clear", "Extrasunny", "Clouds", "Overcast", "Rain", "Clearing", "Thunder", "Smog", "Foggy", "Xmas", "Snowlight", "Blizzard", "Snow", "Halloween", "Neutral" };
        public MenuListItem weatherList;

        private void CreateMenu()
        {
            // Create the menu.
            menu = new Menu(Game.Player.Name, "Weather Options");

            clientSidedWeatherEnabled = new MenuCheckboxItem("Client-Sided Weather", "Enable or disable client-sided weather changes.", false);
            menu.AddMenuItem(clientSidedWeatherEnabled);

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
