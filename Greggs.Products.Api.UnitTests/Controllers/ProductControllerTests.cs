using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Interfaces;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Greggs.Products.Api.Enums;
using Moq;
using Xunit;

namespace Greggs.Products.UnitTests.Controllers;

public class ProductControllerTests
{
    public ICurrencyConverter _currencyConverter { get; set; }

    public ProductControllerTests()
    {
        _currencyConverter = new CurrencyConverter();
    }

    [Fact]
    public void Should_ReturnAllProducts_When_DefaultGet()
    {
        // Arrange
        var mockDataAccess = new Mock<IDataAccess<ProductEntity>>();
        var mockLogger = new Mock<ILogger<ProductController>>();

        mockDataAccess.Setup(dataAccess => dataAccess.List(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(ListOfAllProducts);
        
        var controller = new ProductController(mockDataAccess.Object, _currencyConverter, mockLogger.Object);

        // Act
        var results = controller.Get().ToArray();
        
        // Assert
        results.Length.Should().Be(5);
        
        results[0].Name.Should().Be("Sausage Roll");
        results[0].Price.Should().Be(1m);
        results[0].Currency.Should().Be("GBP");
        results[1].Name.Should().Be("Vegan Sausage Roll");
        results[1].Price.Should().Be(1.1m);
        results[1].Currency.Should().Be("GBP");
        results[2].Name.Should().Be("Steak Bake");
        results[2].Price.Should().Be(1.2m);
        results[2].Currency.Should().Be("GBP");
        results[3].Name.Should().Be("Yum Yum");
        results[3].Price.Should().Be(0.7m);
        results[3].Currency.Should().Be("GBP");
        results[4].Name.Should().Be("Pink Jammie");
        results[4].Price.Should().Be(0.5m);
        results[4].Currency.Should().Be("GBP");
    }
    
    [Fact]
    public void Should_ReturnAllProductsWithPricesInEuros_When_SelectedCurrencyIsEuros()
    {
        // Arrange
        var mockDataAccess = new Mock<IDataAccess<ProductEntity>>();
        var mockLogger = new Mock<ILogger<ProductController>>();

        mockDataAccess.Setup(dataAccess => dataAccess.List(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(ListOfAllProducts);
        
        var controller = new ProductController(mockDataAccess.Object, _currencyConverter, mockLogger.Object);

        // Act
        var results = controller.GetEuros().ToArray();
        
        // Assert
        results.Length.Should().Be(5);
        
        results[0].Name.Should().Be("Sausage Roll");
        
        results.All(result => result.Currency == "EUR").Should().BeTrue();
        
        results[0].Price.Should().Be(_currencyConverter.ConvertFromGBP(1m, CurrencyType.EUR));
        results[1].Price.Should().Be(_currencyConverter.ConvertFromGBP(1.1m, CurrencyType.EUR));
        results[2].Price.Should().Be(_currencyConverter.ConvertFromGBP(1.2m, CurrencyType.EUR));
        results[3].Price.Should().Be(_currencyConverter.ConvertFromGBP(0.7m, CurrencyType.EUR));
        results[4].Price.Should().Be(_currencyConverter.ConvertFromGBP(0.5m, CurrencyType.EUR));
    }

    private IEnumerable<ProductEntity> ListOfAllProducts() =>
        new List<ProductEntity>
        {
            new() { Name = "Sausage Roll", Price = 1m },
            new() { Name = "Vegan Sausage Roll", Price = 1.1m },
            new() { Name = "Steak Bake", Price = 1.2m },
            new() { Name = "Yum Yum", Price = 0.7m },
            new() { Name = "Pink Jammie", Price = 0.5m },
        };
}