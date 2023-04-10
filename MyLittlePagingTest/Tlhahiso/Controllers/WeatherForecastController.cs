using System.Text.Json;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Tlhahiso.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(ILoggerManager logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecastDto> Get([FromQuery] RequestParameters reqParams)
    {
        (IEnumerable<WeatherForecastDto> weatherForecasts, MetaData metaData) result = 
            _weatherService.GetWeatherForecasts(reqParams);
        
        Response.Headers.Add(key: "X-Pagination", JsonSerializer.Serialize(result.metaData));
        
        return result.weatherForecasts;
    }
}
