using Entities;

namespace ServiceContracts.DTO;

public static class BuyOrderExtensions
{
    /// <summary>
    /// An extension method to convert an object of BuyOrder class into BuyOrderResponse class
    /// </summary>
    /// <param name="buyOrder">The BuyOrder object to convert</param>
    /// <returns>Returns the converted BuyOrderResponse object</returns>
    public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
    {
        return new BuyOrderResponse()
        {
            BuyOrderID = buyOrder.BuyOrderId,
            StockSymbol = buyOrder.StockSymbol,
            StockName = buyOrder.StockName,
            Price = buyOrder.Price,
            DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
            Quantity = buyOrder.Quantity,
            TradeAmount = buyOrder.Price * buyOrder.Quantity
        };
    }
}