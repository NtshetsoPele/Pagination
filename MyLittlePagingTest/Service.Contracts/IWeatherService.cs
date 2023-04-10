using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IWeatherService
{
    public (IEnumerable<WeatherForecastDto>, MetaData) GetWeatherForecasts(RequestParameters reqParams);
}