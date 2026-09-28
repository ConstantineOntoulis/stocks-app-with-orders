using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

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
        //Validation: buyOrderRequest can't be null
        if(buyOrderRequest == null)
            throw new ArgumentNullException(nameof(buyOrderRequest));
        
        //Model Validation
        ValidationHelper.ModelValidation(buyOrderRequest);
        
        //Convert buyOrderRequest into BuyOrder type
        BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
        
        //Generate BuyOrderID
        buyOrder.BuyOrderId = Guid.NewGuid();
        
        //Add buy order object to buy orders list
        _buyOrders.Add(buyOrder);
        
        //Convert the BuyOrder object into BuyOrderResponse type
        return buyOrder.ToBuyOrderResponse();
    }

    public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        //Validation: sellOrderRequest can't be null
        if (sellOrderRequest == null)
            throw new ArgumentNullException(nameof(sellOrderRequest));
        
        //Model Validation
        ValidationHelper.ModelValidation(sellOrderRequest);
        
        //Convert sellOrderRequest into SellOrder type
        SellOrder sellOrder = sellOrderRequest.ToSellOrder();
        
        //Generate SellOrderID
        sellOrder.SellOrderID = Guid.NewGuid();
        
        //Add Sell Order object to sell orders list
        _sellOrders.Add(sellOrder);
        
        //Convert the SellOrder Object into SellOrderResponse type
        return sellOrder.ToSellOrderResponse();
    }

    public List<BuyOrderResponse> GetBuyOrders()
    {
        //Convert all BuyOrder objects into BuyOrderResponse objects
        return _buyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).Select(temp => temp.ToBuyOrderResponse())
            .ToList();
    }

    public List<SellOrderResponse> GetSellOrders()
    {
        //Convert all BuyOrder objects into BuyOrderResponse objects
        return _sellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).Select(temp => temp.ToSellOrderResponse())
            .ToList();
    }
}