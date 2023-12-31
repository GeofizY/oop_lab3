﻿using Services.Dtos.Product;
using Services.Dtos.Store;

namespace Services.Interfaces;

public interface IProductService
{
    string Create(string name);
    StoreDto? FindCheapStore(string name);
    StoreDto? FindCollectionInCheapestStore(List<BuyProductDto> entitiesProducts);
}