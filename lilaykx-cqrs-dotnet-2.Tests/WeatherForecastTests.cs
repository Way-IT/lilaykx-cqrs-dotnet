using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
// Подключаем ваш основной проект
using lilaykx_cqrs_dotnet_2;

namespace lilaykx_cqrs_dotnet_2.Tests;

public class WeatherForecastTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WeatherForecastTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsNonEmptyArray()
    {
        var requestUri = "/weatherforecast";

        var response = await _client.GetAsync(requestUri);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var forecasts = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();

        Assert.NotNull(forecasts);
        Assert.NotEmpty(forecasts);
    }
}