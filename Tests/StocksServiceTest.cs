using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace Tests;

public class StocksServiceTest
{
    private readonly IStocksService _stocksService;

    public StocksServiceTest()
    {
        _stocksService = new StocksService();
    }

    #region CreateBuyOrder

    [Fact]
    public void CreateBuyOrder_NullBuyOrder()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = null;

        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory] //[Theory] is preferred over [Fact] because you can pass parameters to the test method
    [InlineData(0)] //Passing parameters to the test method
    public void CreateBuyOrder_QuantityIsLessThanOne(uint buyOrderQuantity)
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = 1,
            Quantity = buyOrderQuantity
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(100001)]
    public void CreateBuyOrder_QuantityIsGreaterThanMaximum(uint buyOrderQuantity)
    {
        BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = 1,
            Quantity = buyOrderQuantity
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateBuyOrder_PriceIsLessThanOne(uint buyOrderPrice)
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = buyOrderPrice,
            Quantity = 1
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(10001)]
    public void CreateBuyOrder_PriceIsGreaterThanMaximum(uint buyOrderPrice)
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = buyOrderPrice,
            Quantity = 1
        };

        //Assert
        Assert.Throws<ArgumentException>(() => { _stocksService.CreateBuyOrder(buyOrderRequest); });
    }

    [Fact]
    public void CreateBuyOrder_StockSymbolIsNull()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = null,
            Price = 1,
            Quantity = 1
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_DateOfOrderIsLessThan2000()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            DateAndTimeOfOrder = Convert.ToDateTime("1999-06-24"),
            Price = 1,
            Quantity = 1
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_ValidBuyOrder()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            DateAndTimeOfOrder = Convert.ToDateTime("2000-05-05"),
            Price = 1,
            Quantity = 1
        };

        //Act
        BuyOrderResponse buyOrderResponseCreate = _stocksService.CreateBuyOrder(buyOrderRequest);

        //Assert
        Assert.NotEqual(Guid.Empty, buyOrderResponseCreate.BuyOrderID);
    }

    #endregion

    #region CreateSellOrder

    

    #endregion
}