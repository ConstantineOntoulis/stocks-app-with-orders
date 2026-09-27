using System.ComponentModel.DataAnnotations;

namespace Entities;

public class BuyOrder
{
    /// <summary>
    /// Represents a buy order to purchase the stocks
    /// </summary>
    [Key]
    public Guid BuyOrderId { get; set; }
    
    /// <summary>
    /// The unique symbol of the stock
    /// </summary>
    public string StockSymbol { get; set; }
    
    /// <summary>
    /// The company's name of the stock
    /// </summary>
    [Required(ErrorMessage = "Stock Name can't be null or empty")]
    public string StockName { get; set; }
    
    /// <summary>
    /// Date and time of order, when it is placed by the user
    /// </summary>
    public DateTime DateAndTimeOfOrder { get; set; }
    
    /// <summary>
    /// The number of stocks to buy
    /// </summary>
    [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares in single order with Minimum of 1.")]
    public uint Quantity { get; set; }
    
    /// <summary>
    /// The price of each stock
    /// </summary>
    [Range(1, 10000, ErrorMessage = "Maximum price of stock is 10000 with Minimum of 1.")]
    public double Price { get; set; } 
}