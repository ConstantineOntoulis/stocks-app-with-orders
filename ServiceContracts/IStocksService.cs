using ServiceContracts.DTO;

namespace ServiceContracts;

public interface IStocksService
{
    /// <summary>
    /// Creates a buy order
    /// </summary>
    /// <param name="buyOrderRequest">Buy order object</param>
    BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest);
    
    /// <summary>
    /// Creates a buy order
    /// </summary>
    /// <param name="sellOrderRequest">Sell order object</param>
    SellOrderResponse CreateSellOrder(SellOrderResponse? sellOrderRequest);

    /// <summary>
    /// Returns all existing buy orders
    /// </summary>
    /// <returns>Returns a list of objects of BuyOrder type</returns>
    List<BuyOrderResponse> GetBuyOrders();
    
    /// <summary>
    /// Returns all existing sell orders
    /// </summary>
    /// <returns>Returns a list of objects of SellOrder type</returns>
    List<SellOrderResponse> GetSellOrders();
}