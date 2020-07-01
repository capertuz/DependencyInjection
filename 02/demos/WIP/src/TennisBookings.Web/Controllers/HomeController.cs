using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;

namespace TennisBookings.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWeatherForecaster _weatherForecaster;
        public string WeatherDescription { get; private set; } =
            "We don't have the latest weather information right now, please check again later.";

        public HomeController(IWeatherForecaster weatherForecaster)
        {
            _weatherForecaster = weatherForecaster;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel();

            var currentWeather = await _weatherForecaster.GetCurrentWeatherAsync();
            switch (currentWeather.Description)
            {
                case "Sun":
                    WeatherDescription = "It's sunny right now. A great day for tennis!";
                    break;

                case "Cloud":
                    WeatherDescription = "It's cloudy at the moment and the outdoor courts are in use.";
                    break;

                case "Rain":
                    WeatherDescription = "We're sorry but it's raining here. No outdoor courts in use.";
                    break;

                case "Snow":
                    WeatherDescription = "It's snowing!! Outdoor courts will remain closed until the snow has cleared.";
                    break;
            }

            viewModel.WeatherDescription = WeatherDescription;

            return View(viewModel);
        }
    }
}