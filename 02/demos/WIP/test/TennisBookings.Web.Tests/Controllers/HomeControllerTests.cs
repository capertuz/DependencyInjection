using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using TennisBookings.Web.Controllers;
using TennisBookings.Web.Domain;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;
using Xunit;

namespace TennisBookings.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSun()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            var temp = new CurrentWeatherResult
            {
                Description = "Sun"
            };
            mockWeatherForecaster.Setup(w => w.GetCurrentWeatherAsync()).Returns(
               Task.FromResult(temp)
                );

            var sut = new HomeController(mockWeatherForecaster.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now.", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsRain()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            var temp = new CurrentWeatherResult
            {
                Description = "Rain"
            };
            mockWeatherForecaster.Setup(w => w.GetCurrentWeatherAsync()).Returns(
                Task.FromResult(temp)
            );
            var sut = new HomeController(mockWeatherForecaster.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("We're sorry but it's raining here.", model.WeatherDescription);
        }
    }
}
