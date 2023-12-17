using Services.Dtos.Product;

namespace Services.Interfaces;

public interface IStoreService
{
    int Create(string name, string address);
    decimal BuyProducts(int id, List<BuyProductDto> dtos);
    void ProductsDelivery(int id, List<ProductsDeliveryDto> dtos);
}