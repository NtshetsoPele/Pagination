using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class WeatherService : IWeatherService
{
    private static readonly string[] Summaries;
    
    static WeatherService()
    {
        Summaries = new[] 
        {
            "Freezing", "Bracing", "Chilly", "Cool",       "Mild", 
            "Warm",     "Balmy",   "Hot",    "Sweltering", "Scorching",
            "Rainy",    "Wet",     "Humid",  "Dry",        "Arid", 
            "Frigid",   "Foggy",   "Windy",  "Stormy",     "Breezy", 
            "Windless", "Calm",    "Still"
        };
    }
    
    public (IEnumerable<WeatherForecastDto>, MetaData) GetWeatherForecasts(RequestParameters reqParams)
    {
        var metaData = new MetaData
        {
            CurrentPage = reqParams.PageNumber,
            TotalPages = (int)Math.Ceiling(Summaries.Length / (double)reqParams.PageSize),
            PageSize = reqParams.PageSize,
            TotalCount = Summaries.Length
        };
        
        var forecasts = Enumerable.Range(1, Summaries.Length)
            .Select(
                (int index) => new WeatherForecastDto
                {
                    Date         = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary      = Summaries[Random.Shared.Next(Summaries.Length)]
                })
            .Skip(count: (reqParams.PageNumber - 1) * reqParams.PageSize)
            .Take(count: reqParams.PageSize);

        return (forecasts, metaData);
    }
}