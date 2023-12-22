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
    public void Create([FromBody]StoreDto entityStore)
    {
        _storeService.Create(entityStore.Name, entityStore.Address);
    }

    [HttpPost("/purchase/{storeid}")]
    public IActionResult BuyProducts([FromRoute] int storeid, [FromBody] List<BuyProductDto> entitiesProducts)
    {
        var total = _storeService.BuyProducts(storeid, entitiesProducts);
        if (total == -404)
            return Ok("Операция невозможна\n(Требуемое количество товаров больше, чем есть на складе)");
        else
            return Ok($"Стоимость заказа: {total}");
    }

    [HttpPost("/delivery/{storeid}")]
    public void ProductsDelivery([FromRoute]int storeid,[FromBody]List<ProductsDeliveryDto> entitiesProducts)
    {
        _storeService.ProductsDelivery(storeid, entitiesProducts);
    }
    
}