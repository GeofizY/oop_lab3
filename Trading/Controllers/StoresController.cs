using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Product;
using Services.Dtos.Store;
using Services.Interfaces;

namespace Trading.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
    private IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpPost]
    public void Create([FromBody]StoreDto dto)
    {
        _storeService.Create(dto.Name, dto.Address);
    }

    [HttpPost("/purchase/{storeid}")]
    public IActionResult BuyProducts([FromRoute] int storeid, [FromBody] List<BuyProductDto> dtos)
    {
        var total = _storeService.BuyProducts(storeid, dtos);
        if (total == -404)
            return Ok("Операция невозможна\n(Требуемое количество товаров больше, чем есть на складе)");
        else
            return Ok($"Стоимость заказа: {total}");
    }

    [HttpPost("/delivery/{notwork}")] // пока не работает
    public void ProductsDelivery([FromRoute]int notwork,[FromBody]List<ProductsDeliveryDto> dto)
    {
        _storeService.ProductsDelivery(notwork, dto);
    }
    
}