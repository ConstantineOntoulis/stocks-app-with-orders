using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceContracts;

namespace Services;

public class FinnhubService : IFinnhubService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public FinnhubService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public Dictionary<string, object>? GetCompanyProfile(string stockSymbol)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient();

        string token = _configuration["FinnhubToken"]
            ?? throw new InvalidOperationException(
                "FinnhubToken was not found.");

        HttpRequestMessage request = new HttpRequestMessage()
        {
            RequestUri = new Uri(
                $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}"),

            Method = HttpMethod.Get
        };

        HttpResponseMessage response = httpClient.Send(request);

        Stream stream = response.Content.ReadAsStream();

        using StreamReader streamReader = new StreamReader(stream);

        string responseBody = streamReader.ReadToEnd();

        Dictionary<string, object>? responseDictionary =
            JsonSerializer.Deserialize<Dictionary<string, object>>(
                responseBody);

        if (responseDictionary == null)
        {
            throw new InvalidOperationException(
                "No response from Finnhub server.");
        }

        if (responseDictionary.ContainsKey("error"))
        {
            throw new InvalidOperationException(
                Convert.ToString(responseDictionary["error"]));
        }

        return responseDictionary;
    }

    public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient();

        string token = _configuration["FinnhubToken"]
            ?? throw new InvalidOperationException(
                "FinnhubToken was not found.");

        HttpRequestMessage request = new HttpRequestMessage()
        {
            RequestUri = new Uri(
                $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}"),

            Method = HttpMethod.Get
        };

        HttpResponseMessage response = httpClient.Send(request);

        Stream stream = response.Content.ReadAsStream();

        using StreamReader streamReader = new StreamReader(stream);

        string responseBody = streamReader.ReadToEnd();

        Dictionary<string, object>? responseDictionary =
            JsonSerializer.Deserialize<Dictionary<string, object>>(
                responseBody);

        if (responseDictionary == null)
        {
            throw new InvalidOperationException(
                "No response from Finnhub server.");
        }

        if (responseDictionary.ContainsKey("error"))
        {
            throw new InvalidOperationException(
                Convert.ToString(responseDictionary["error"]));
        }

        return responseDictionary;
    }
}