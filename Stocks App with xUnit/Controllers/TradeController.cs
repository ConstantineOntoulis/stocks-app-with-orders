using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Stocks_App_with_xUnit.Models;

namespace Stocks_App_with_xUnit.Controllers;

public class TradeController : Controller
{
    private readonly IFinnhubService _finnhubService;
    private readonly IStocksService _stocksService;
    private readonly IOptions<TradingOptions> _tradingOptions;
    private readonly IConfiguration _configuration;

    public TradeController(
        IFinnhubService finnhubService,
        IStocksService stocksService,
        IOptions<TradingOptions> tradingOptions,
        IConfiguration configuration)
    {
        _finnhubService = finnhubService;
        _stocksService = stocksService;
        _tradingOptions = tradingOptions;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("/")]
    [Route("/Trade/Index")]
    public IActionResult Index()
    {
        string stockSymbol =
            (_tradingOptions.Value.DefaultStockSymbol ?? "NVDA").Trim();

        Dictionary<string, object>? companyProfile =
            _finnhubService.GetCompanyProfile(stockSymbol);

        Dictionary<string, object>? stockQuote =
            _finnhubService.GetStockPriceQuote(stockSymbol);

        if (companyProfile == null || stockQuote == null)
        {
            return Problem(
                "Stock information could not be retrieved.");
        }

        StockTrade stockTrade = new StockTrade()
        {
            StockSymbol = stockSymbol,

            StockName =
                Convert.ToString(companyProfile["name"]),

            Price =
                Convert.ToDouble(
                    stockQuote["c"].ToString(),
                    CultureInfo.InvariantCulture),

            Quantity = 0
        };

        ViewBag.FinnhubToken =
            _configuration["FinnhubToken"];

        return View(stockTrade);
    }

    [HttpGet]
    [Route("/stock-details")]
    public IActionResult Details()
    {
        string stockSymbol =
            (_tradingOptions.Value.DefaultStockSymbol ?? "NVDA").Trim();

        Dictionary<string, object>? stockQuote =
            _finnhubService.GetStockPriceQuote(stockSymbol);

        if (stockQuote == null)
        {
            return Problem(
                "Stock information could not be retrieved.");
        }

        StockDetails stockDetails = new StockDetails()
        {
            StockSymbol = stockSymbol,

            CurrentPrice =
                Convert.ToDouble(
                    stockQuote["c"].ToString(),
                    CultureInfo.InvariantCulture),

            Change =
                Convert.ToDouble(
                    stockQuote["d"].ToString(),
                    CultureInfo.InvariantCulture),

            PercentChange =
                Convert.ToDouble(
                    stockQuote["dp"].ToString(),
                    CultureInfo.InvariantCulture),

            LowestPrice =
                Convert.ToDouble(
                    stockQuote["l"].ToString(),
                    CultureInfo.InvariantCulture),

            HighestPrice =
                Convert.ToDouble(
                    stockQuote["h"].ToString(),
                    CultureInfo.InvariantCulture),

            OpenPrice =
                Convert.ToDouble(
                    stockQuote["o"].ToString(),
                    CultureInfo.InvariantCulture),

            PreviousClosePrice =
                Convert.ToDouble(
                    stockQuote["pc"].ToString(),
                    CultureInfo.InvariantCulture)
        };

        ViewBag.FinnhubToken =
            _configuration["FinnhubToken"];

        return View(stockDetails);
    }
}