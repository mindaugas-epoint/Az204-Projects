using Newtonsoft.Json.Linq;
using WeatherTracker.Models;

namespace WeatherTracker.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        public WeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<WeatherForecast> GetForecastAsync(string city)
        {
            string? baseUrl = _configuration["WeatherForecastApi_BaseUrl"]; 
            string? apiKey = _configuration["WeatherForecastApi_ApiKey"];

            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException($"Weather forecast API configuration is missing. {Environment.NewLine} You need to set environment variables for the WeatherForecastApi_BaseUrl and WeatherForecastApi_ApiKey");
            }

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            var result = await client.GetAsync($"current.json?key={apiKey}&q={city}");
            if (result?.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return await Task.FromResult(new WeatherForecast());
            } 

            var content = await result.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
            {
                return await Task.FromResult(new WeatherForecast());
            }

            JObject jsonObject = JObject.Parse(content);

            var forecast = new WeatherForecast()
            {
                Location = new Location()
                {
                    Name = jsonObject.GetValue("location")?.Value<string>("name"),
                    Region = jsonObject.GetValue("location")?.Value<string>("region"),
                    Country = jsonObject.GetValue("location")?.Value<string>("country"),
                    Lat = (double)(jsonObject.GetValue("location")?.Value<double>("lat")),
                    Lon = (double)(jsonObject.GetValue("location")?.Value<double>("lon")),
                    TzId = jsonObject.GetValue("location")?.Value<string>("tz_id"),
                    LocaltimeEpoch = (long)(jsonObject.GetValue("location")?.Value<long>("localtime_epoch")),
                    Localtime = jsonObject.GetValue("location")?.Value<string>("localtime")
                },
                Current = new Current()
                {
                    LastUpdatedEpoch = (long)(jsonObject.GetValue("current")?.Value<long>("last_updated_epoch")),
                    LastUpdated = jsonObject.GetValue("current")?.Value<string>("last_updated"),
                    TempC = (double)(jsonObject.GetValue("current")?.Value<double>("temp_c")),
                    TempF = (double)(jsonObject.GetValue("current")?.Value<double>("temp_f")),
                    IsDay = (int)(jsonObject.GetValue("current")?.Value<int>("is_day")),
                    WindMph = (double)(jsonObject.GetValue("current")?.Value<double>("wind_mph")),
                    WindKph = (double)(jsonObject.GetValue("current")?.Value<double>("wind_kph")),
                    WindDegree = (int)(jsonObject.GetValue("current")?.Value<int>("wind_degree")),
                    WindDir = jsonObject.GetValue("current")?.Value<string>("wind_dir"),
                    PressureMb = (double)(jsonObject.GetValue("current")?.Value<double>("pressure_mb")),
                    PressureIn = (double)(jsonObject.GetValue("current")?.Value<double>("pressure_in")),
                    PrecipMm = (double)(jsonObject.GetValue("current")?.Value<double>("precip_mm")),
                    PrecipIn = (double)(jsonObject.GetValue("current")?.Value<double>("precip_in")),
                    Humidity = (int)(jsonObject.GetValue("current")?.Value<int>("humidity")),
                    Cloud = (int)(jsonObject.GetValue("current")?.Value<int>("cloud")),
                    FeelslikeC = (double)(jsonObject.GetValue("current")?.Value<double>("feelslike_c")),
                    FeelslikeF = (double)(jsonObject.GetValue("current")?.Value<double>("feelslike_f")),
                    VisKm = (double)(jsonObject.GetValue("current")?.Value<double>("vis_km")),
                    VisMiles = (double)(jsonObject.GetValue("current")?.Value<double>("vis_miles")),
                    Uv = (double)(jsonObject.GetValue("current")?.Value<double>("uv")),
                    GustMph = (double)(jsonObject.GetValue("current")?.Value<double>("gust_mph")),
                    GustKph = (double)(jsonObject.GetValue("current")?.Value<double>("gust_kph"))
                }
            };

            if (forecast == null)
            {
                return await Task.FromResult(new WeatherForecast());
            }

            return forecast;
        }
    }
}
