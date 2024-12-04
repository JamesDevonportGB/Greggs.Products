using Greggs.Products.Api.Enums;

namespace Greggs.Products.Api.Interfaces;

public interface ICurrencyConverter
{
    /// <summary>
    /// Get the price in the user's selected currency
    /// </summary>
    /// <param name="basePrice">Should be in GBP</param>
    /// <param name="destinationCurrency">The user's selected currency</param>
    /// <returns></returns>
    decimal ConvertFromGBP(decimal basePrice, CurrencyType destinationCurrency);
}