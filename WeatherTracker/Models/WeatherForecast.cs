namespace WeatherTracker.Models
{
    public class WeatherForecast
    {
        public Location? Location { get; init; }
        public Current? Current { get; init; }
    }

    public record Location
    {
        public string? Name { get; init; }
        public string? Region { get; init; }
        public string? Country { get; init; }
        public double? Lat { get; init; }
        public double? Lon { get; init; }
        public string? TzId { get; init; }
        public long? LocaltimeEpoch { get; init; }
        public string? Localtime { get; init; }
    }

    public record Current
    {
        public long LastUpdatedEpoch { get; init; }
        public string? LastUpdated { get; init; }
        public double TempC { get; init; }
        public double TempF { get; init; }
        public int IsDay { get; init; }
        public double WindMph { get; init; }
        public double WindKph { get; init; }
        public int WindDegree { get; init; }
        public string? WindDir { get; init; }
        public double PressureMb { get; init; }
        public double PressureIn { get; init; }
        public double PrecipMm { get; init; }
        public double PrecipIn { get; init; }
        public int Humidity { get; init; }
        public int Cloud { get; init; }
        public double FeelslikeC { get; init; }
        public double FeelslikeF { get; init; }
        public double VisKm { get; init; }
        public double VisMiles { get; init; }
        public double Uv { get; init; }
        public double GustMph { get; init; }
        public double GustKph { get; init; }
    }
}