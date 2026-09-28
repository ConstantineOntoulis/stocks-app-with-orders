using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class StocksService : IStocksService
{
    private readonly List<BuyOrder> _buyOrders;
    private readonly List<SellOrder> _sellOrders;

    /// <summary>
    /// Constructor of StocksService class that executes when a new object is created for the class
    /// </summary>
    public StocksService()
    {
        _buyOrders = new List<BuyOrder>();
        _sellOrders = new List<SellOrder>();
    }

    public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        throw new NotImplementedException();
    }

    public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        throw new NotImplementedException();
    }

    public List<BuyOrderResponse> GetBuyOrders()
    {
        throw new NotImplementedException();
    }

    public List<SellOrderResponse> GetSellOrders()
    {
        throw new NotImplementedException();
    }
}