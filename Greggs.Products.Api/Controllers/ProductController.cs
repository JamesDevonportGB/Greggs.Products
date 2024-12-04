using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Interfaces;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IDataAccess<ProductEntity> _productService;
    private readonly ICurrencyConverter _currencyConverter;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IDataAccess<ProductEntity> productService,
                                ICurrencyConverter currencyConverter,
                                ILogger<ProductController> logger)
    {
        _productService = productService;
        _currencyConverter = currencyConverter;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<ProductViewModel> Get(int pageStart = 0, int pageSize = 5)
    {
        var entities = _productService.List(pageStart, pageSize);
        var viewModel = MapToViewModel(entities);

        return viewModel;
    }
    
    [HttpGet]
    [Route("eur")]
    public IEnumerable<ProductViewModel> GetEuros(int pageStart = 0, int pageSize = 5)
    {
        var entities = _productService.List(pageStart, pageSize);
        var viewModel = MapToViewModel(entities, CurrencyType.EUR);

        return viewModel;
    }

    #region Internal methods

    private IEnumerable<ProductViewModel> MapToViewModel(IEnumerable<ProductEntity> entities, CurrencyType expectedCurrency = CurrencyType.GBP) =>
        entities.Select(product => new ProductViewModel
        {
            Name = product.Name,
            Price = _currencyConverter.ConvertFromGBP(product.Price, expectedCurrency),
            Currency = expectedCurrency.ToString()
        });

    #endregion
}