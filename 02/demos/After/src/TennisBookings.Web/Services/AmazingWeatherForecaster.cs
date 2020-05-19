
namespace TennisBookings.Web.Services
{
    public class AmazingWeatherForecaster : IWeatherForecaster
    {
        public WeatherResult GetCurrentWeather()
        {
            // DO SOMETHING AMAZING HERE!!!

            return new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun
            };
        }
    }
}
