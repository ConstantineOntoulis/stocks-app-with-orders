using Entities;

namespace ServiceContracts.DTO;

public static class SellOrderExtensions
{
    /// <summary>
    /// An extension method to convert an object of SellOrder class into SellOrderResponse class
    /// </summary>
    /// <param name="sellOrder">The SellOrder object to convert</param>
    /// <returns>Returns the converted SellOrderResponse object</returns>
    public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
    {
        return new SellOrderResponse() { SellOrderID = sellOrder.SellOrderID, StockSymbol = sellOrder.StockSymbol, StockName = sellOrder.StockName, Price = sellOrder.Price, DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder, Quantity = sellOrder.Quantity, TradeAmount = sellOrder.Price * sellOrder.Quantity };
    }
}