using Microsoft.AspNetCore.Mvc;
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
        _storeService.Create(dto.StoreId, dto.Name, dto.Address);
    }
    
}