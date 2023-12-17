using DAL;
using Models;
using Services.Dtos.Product;
using Services.Interfaces;

namespace Services.Realization;

public class StoreService : IStoreService
{
    private StoreDbContext _dbContext;

    public StoreService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Create(string name, string address)
    {
        var store = new Store { Name = name, Address = address };
        var newStore = _dbContext.Stores.Add(store);
        _dbContext.SaveChanges();

        return newStore.Entity.StoreId;
    }

    public decimal BuyProducts(int id, List<BuyProductDto> dtos)
    {
        var products = _dbContext.ProductsInStore
            .Where(x => x.StoreId == id)
            .Where(x => dtos.Select(y => y.ProductName).Contains(x.ProductName));

        decimal total = 0;
        foreach (var product in products)
        {
            var neededQuantity = dtos.FirstOrDefault(x => x.ProductName == product.ProductName).Quantity;
            if (product.Quantity >= neededQuantity)
            {
                total += product.Price * neededQuantity;
                product.Quantity -= neededQuantity;
            }
            else
            {
                total = -404;
                break;
            }
        }
        
        if (total != -404)
            _dbContext.SaveChanges();
        return total;
    }

    public void ProductsDelivery(int id, List<ProductsDeliveryDto> dtos)
    {
        var store = _dbContext.Stores.FirstOrDefault(x => x.StoreId == id);
        // не доделал
    }
    
}