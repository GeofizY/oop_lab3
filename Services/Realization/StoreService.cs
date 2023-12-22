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

        return newStore.Entity.StoreId+1;
    }

    public decimal BuyProducts(int id, List<BuyProductDto> entitiesProducts)
    {
        var products = _dbContext.ProductsInStore
            .Where(x => x.StoreId == id)
            .Where(x => entitiesProducts.Select(y => y.ProductName).Contains(x.ProductName));

        decimal total = 0;
        foreach (var product in products)
        {
            var neededQuantity = entitiesProducts.FirstOrDefault(x => x.ProductName == product.ProductName).Quantity;
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

    public void ProductsDelivery(int id, List<ProductsDeliveryDto> entitiesProducts)
    {
        var store = _dbContext.Stores.FirstOrDefault(x => x.StoreId == id);

        if (store == null) return;

        var storeProducts = entitiesProducts.Select(x => new ProductInStore
        {
            StoreId = id,
            ProductName = x.ProductName,
            Price = x.Price,
            Quantity = x.Quantity
        }).ToList().Distinct();

        var existProducts = _dbContext.ProductsInStore
            .Where(x => x.StoreId == id)
            .Where(x => entitiesProducts.Select(y => y.ProductName).Contains(x.ProductName));

        var newProducts = storeProducts.ExceptBy(existProducts.Select(x => x.ProductName), x => x.ProductName);
        _dbContext.ProductsInStore.AddRange(newProducts);

        foreach (var existProduct in existProducts)
        {
            existProduct.Quantity += storeProducts.FirstOrDefault(y => y.ProductName == existProduct.ProductName).Quantity;
            existProduct.Price = storeProducts.FirstOrDefault(y => y.ProductName == existProduct.ProductName).Price;
        }

        _dbContext.SaveChanges();
    }
    
}