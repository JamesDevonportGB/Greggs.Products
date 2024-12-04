using Greggs.Products.Api.Enums;

namespace Greggs.Products.Api.Models;

public class ProductEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }  // It should be assumed that this value is GBP
}