using WeatherTracker.Models;

namespace WeatherTracker.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetForecastAsync(string city);
    }
}
