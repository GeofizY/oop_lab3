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
    public void Create([FromBody] ProductDto dto)
    {
        _productService.Create(dto.Name);
    }
        
}