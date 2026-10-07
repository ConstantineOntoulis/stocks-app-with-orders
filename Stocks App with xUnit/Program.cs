using ServiceContracts;
using Services;
using Stocks_App_with_xUnit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions"));

builder.Services.AddHttpClient();

builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();