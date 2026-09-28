namespace StocksApp_Assignment.Models;

public class StockDetails
{
    public string? StockSymbol { get; set; }

    public double CurrentPrice { get; set; }

    public double Change { get; set; }

    public double PercentChange { get; set; }

    public double LowestPrice { get; set; }

    public double HighestPrice { get; set; }

    public double PreviousClosePrice { get; set; }
}