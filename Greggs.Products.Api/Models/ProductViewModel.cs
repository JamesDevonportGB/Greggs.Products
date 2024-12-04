using Greggs.Products.Api.Enums;

namespace Greggs.Products.Api.Models;

public class ProductViewModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }          // This is local pricing, matched to the Currency property
    
    public string Currency { get; set; }
}