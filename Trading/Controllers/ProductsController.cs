using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Product;
using Services.Interfaces;

namespace Trading.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public void Create([FromBody] ProductDto entityProduct)
    {
        _productService.Create(entityProduct.Name);
    }

    [HttpGet("/cheap/{name}")]
    public IActionResult FindCheapStore([FromRoute] string name)
    {
        var store = _productService.FindCheapStore(name);
        if (store != null)
           return Ok(store);
        else
           return BadRequest("Товар не найден ни в одном магазине");

    }
        
}