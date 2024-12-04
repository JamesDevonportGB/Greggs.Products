using FluentAssertions;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Interfaces;
using Greggs.Products.Api.Services;
using Xunit;

namespace Greggs.Products.UnitTests.Services;

public class CurrencyConverterTests
{
    public ICurrencyConverter _currencyConverter { get; set; }

    public CurrencyConverterTests()
    {
        _currencyConverter = new CurrencyConverter();
    }

    [Fact]
    public void CurrencyConverterTestHealthcheck()
    {
        _currencyConverter.Should().BeOfType<CurrencyConverter>();
        _currencyConverter.Should().BeAssignableTo<ICurrencyConverter>();
        _currencyConverter.Should().NotBeNull();
    }
    
    [Fact]
    public void Should_ReturnSamePrice_When_ExpectedCurrencyIsGBP()
    {
        // Arrange
        var basePrice = 100m;
        
        // Act
        var result = _currencyConverter.ConvertFromGBP(basePrice, CurrencyType.GBP);
        
        // Assert
        result.Should().Be(basePrice);
    }
    
    [Fact]
    public void Should_ConvertToEuros_When_ExpectedCurrencyIsEUR()
    {
        // Arrange
        var basePrice = 100m;
        var expectedPrice = 111m;
        
        // Act
        var result = _currencyConverter.ConvertFromGBP(basePrice, CurrencyType.EUR);
        
        // Assert
        result.Should().Be(expectedPrice);
    }
}
