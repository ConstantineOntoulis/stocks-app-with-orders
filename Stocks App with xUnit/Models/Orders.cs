using ServiceContracts.DTO;

namespace Stocks_App_with_xUnit.Models;

public class Orders
{
    public List<BuyOrderResponse> BuyOrders { get; set; } = new List<BuyOrderResponse>();
    public List<SellOrderResponse> SellOrders { get; set; } = new List<SellOrderResponse>();
}