using System;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Interfaces;

namespace Greggs.Products.Api.Services;

public class CurrencyConverter : ICurrencyConverter
{
    private const decimal _euroConversionFactor = 1.11M;    // This could be hardcoded, or come from an external exchange etc.
    
    public decimal ConvertFromGBP(decimal basePrice, CurrencyType destinationCurrency)
    {
        var result = destinationCurrency switch
        {
            CurrencyType.GBP => basePrice,
            CurrencyType.EUR => basePrice * _euroConversionFactor,
            _ => throw new Exception("Cannot convert to unexpected currency type")    // Tranditionally this would log and then soft-fail
        };
        
        result = Math.Round(result, 2); // We want only two decimal places
        
        return result;
    }
}
