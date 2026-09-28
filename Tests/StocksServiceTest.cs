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

    [Fact]
    public void CreateSellOrder_NullSellOrder()
    {
        //Arrange 
        SellOrderRequest? sellOrderRequest = null;
        
        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateSellOrder_QuantityIsLessThanOne(uint sellOrderQuantity)
    {
        //Arrange
        SellOrderRequest? sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = 1,
            Quantity = sellOrderQuantity
        };
        
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(100001)]
    public void CreateSellOrder_QuantityIsGreaterThanMaximum(uint sellOrderQuantity)
    {
        SellOrderRequest? sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = 1,
            Quantity = sellOrderQuantity
        };
        
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateSellOrder_PriceIsLessThanOne(uint sellOrderPrice)
    {
        //Arrange 
        SellOrderRequest? sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = sellOrderPrice,
            Quantity = 1
        };
        
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(10001)]
    public void CreateSellOrder_PriceIsGreaterThanMaximum(uint sellOrderPrice)
    {
        //Arrange
        SellOrderRequest? sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = sellOrderPrice,
            Quantity = 1
        };
        
        //Act
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_StockSymbolIsNull()
    {
        SellOrderRequest? sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = null,
            Price = 1,
            Quantity = 1
        };
        
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_DateIsLessThan2000()
    {
        SellOrderRequest? sellOrderRequest = new SellOrderRequest()
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
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_ValidSellOrder()
    {
        //Arrange
        SellOrderRequest? sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            DateAndTimeOfOrder = Convert.ToDateTime("2000-05-05"),
            Price = 1,
            Quantity = 1
        };
        
        //Act
        SellOrderResponse sellOrderResponseCreate = _stocksService.CreateSellOrder(sellOrderRequest);
        
        //Assert
        Assert.NotEqual(Guid.Empty, sellOrderResponseCreate.SellOrderID);
    }
    
    #endregion

    #region GetBuyOrders

    //The GetAllBuyOrders() should return an empty list by default
    [Fact]
    public void GetAllBuyOrders_DefaultList()
    {
        //Act
        List<BuyOrderResponse> buyOrdersGet =  _stocksService.GetBuyOrders();
        
        //Assert
        Assert.Empty(buyOrdersGet);
    }

    [Fact]
    public void GetAllBuyOrders_WithFewBuyOrders()
    {
        //Arrange
        
        //Create a list of buy orders with hard-coded data
        BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = 1,
            Quantity = 1,
            DateAndTimeOfOrder = DateTime.Parse("2000-05-05 10:00")
        };
        
        BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = 1,
            Quantity = 1,
            DateAndTimeOfOrder = DateTime.Parse("2000-05-05 10:00")
        };

        List<BuyOrderRequest> buyOrderResponse_List_Add = new List<BuyOrderRequest>()
        {
            buyOrderRequest1,
            buyOrderRequest2
        };

        List<BuyOrderResponse> buyOrders_response = new List<BuyOrderResponse>();

        foreach (BuyOrderRequest buyOrderRequest in buyOrderResponse_List_Add)
        {
            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
            buyOrders_response.Add(buyOrderResponse);
        }
        
        //Act
        List<BuyOrderResponse> buyOrderResponse_Get = _stocksService.GetBuyOrders();
        
        //Assert
        foreach (BuyOrderResponse buyOrder_response_from_add in buyOrders_response)
        {
            Assert.Contains(buyOrder_response_from_add, buyOrderResponse_Get);
        }
    }

    #endregion

    #region GetSellOrders

    [Fact]
    public void GetSellOrders_DefaultList()
    {
        //Act
        List<SellOrderResponse> sellOrdersGet = _stocksService.GetSellOrders();
        
        //Assert
        Assert.Empty(sellOrdersGet);
    }

    [Fact]
    public void GetSellOrders_WithFewSellOrders()
    {
        //Arrange
        
        //Create a list of sell orders with hard-coded data
        SellOrderRequest sellOrderRequest1 = new SellOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = 1,
            Quantity = 1,
            DateAndTimeOfOrder = DateTime.Parse("2000-05-05 10:00")
        };
        
        SellOrderRequest sellOrderRequest2 = new SellOrderRequest()
        {
            StockSymbol = "NVDA",
            StockName = "nVidia",
            Price = 1,
            Quantity = 1,
            DateAndTimeOfOrder = DateTime.Parse("2000-05-05 10:00")
        };
        
        List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>()
        {
            sellOrderRequest1,
            sellOrderRequest2
        };

        List<SellOrderResponse> sellOrderResponse_ListForAdd = new List<SellOrderResponse>();

        foreach (SellOrderRequest sellOrderRequest in sellOrderRequests)
        {
            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
            sellOrderResponse_ListForAdd.Add(sellOrderResponse);
        }
        
        //Act
        List<SellOrderResponse> sellOrderResponse_Get = _stocksService.GetSellOrders();
        
        //Assert
        foreach (SellOrderResponse sellOrderResponse_from_add in sellOrderResponse_ListForAdd)
        {
            Assert.Contains(sellOrderResponse_from_add, sellOrderResponse_Get);
        }
    }

    #endregion
}