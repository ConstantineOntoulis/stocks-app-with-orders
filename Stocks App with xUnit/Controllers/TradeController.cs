using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
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
    public async Task<IActionResult> Index()
    {
        string stockSymbol =
            (_tradingOptions.Value.DefaultStockSymbol ?? "NVDA").Trim();

        Dictionary<string, object>? companyProfile =
            await _finnhubService.GetCompanyProfile(stockSymbol);

        Dictionary<string, object>? stockQuote =
            await _finnhubService.GetStockPriceQuote(stockSymbol);

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

            Quantity = _tradingOptions.Value.DefaultOrderQuantity
        };

        ViewBag.FinnhubToken =
            _configuration["FinnhubToken"];

        return View(stockTrade);
    }

    [HttpGet]
    [Route("/stock-details")]
    public async Task<IActionResult> Details()
    {
        string stockSymbol =
            (_tradingOptions.Value.DefaultStockSymbol ?? "NVDA").Trim();

        Dictionary<string, object>? stockQuote =
            await _finnhubService.GetStockPriceQuote(stockSymbol);

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

    [HttpPost]
    [Route("/Trade/BuyOrder")]
    public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
    {
        //mutation: some piece of the state of the object changes
        buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

        ModelState.Clear();
        TryValidateModel(buyOrderRequest);

        if (!ModelState.IsValid)
        {
            ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(m => m.ErrorMessage).ToList();
            StockTrade stockTrade = new StockTrade()
            {
                StockName = buyOrderRequest.StockName,
                Quantity = buyOrderRequest.Quantity,
                StockSymbol = buyOrderRequest.StockSymbol,
                Price = buyOrderRequest.Price
            };
            return View("Index", stockTrade);
        }

        BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
        return RedirectToAction(nameof(Orders));
    }

    [HttpPost]
    [Route("/Trade/SellOrder")]
    public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
    {
        //mutation: some piece of the state of the object changes
        sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

        ModelState.Clear();
        TryValidateModel(sellOrderRequest);

        if (!ModelState.IsValid)
        {
            ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(m => m.ErrorMessage).ToList();
            StockTrade stockTrade = new StockTrade()
            {
                StockName = sellOrderRequest.StockName,
                Quantity = sellOrderRequest.Quantity,
                StockSymbol = sellOrderRequest.StockSymbol,
                Price = sellOrderRequest.Price
            };

            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View("Index", stockTrade);
        }

        SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
        return RedirectToAction(nameof(Orders));
    }

    [HttpGet]
    [Route("/Trade/Orders")]
    public async Task<IActionResult> Orders()
    {
        List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();
        List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

        Orders orders = new Orders()
        {
            BuyOrders = buyOrderResponses,
            SellOrders = sellOrderResponses
        };

        return View(orders);
    }
}