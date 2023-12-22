using System.Security.Cryptography.X509Certificates;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Dtos.Product;
using Services.Dtos.Store;
using Services.Interfaces;

namespace Services.Realization;

public class ProductService : IProductService
{
    private StoreDbContext _dbContext;

    public ProductService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string Create(string name)
    {
        var product = new Product { Name = name };
        var newProduct = _dbContext.Products.Add(product);
        _dbContext.SaveChanges();

        return newProduct.Entity.Name;
    }

    public StoreDto? FindCheapStore(string name)
    {
        var store = _dbContext.ProductsInStore
            .Include(x => x.Store)
            .Where(x => x.ProductName == name)
            .OrderBy(x => x.Price)
            .FirstOrDefault();

        if (store == null)
            return new StoreDto();

        return new StoreDto() { Name = store.Store.Name, Address = store.Store.Address };
    }

    public StoreDto? FindCollectionInCheapestStore(List<BuyProductDto> entitiesProducts)
    {



        var cheap = new List <(int store, decimal total ) >();
        
        foreach (var store in _dbContext.Stores.Include(x => x.ProductsInStore))
        {
            bool flag = true;
            decimal total = 0;
            foreach (var product in entitiesProducts)
            {
                var productInStore = store.ProductsInStore.FirstOrDefault(x => x.ProductName == product.ProductName);

                if (!(productInStore != null && productInStore.Quantity >= product.Quantity))
                {
                    flag = false;
                    break;
                }

                total += productInStore.Price * product.Quantity;
            }

            if (flag)
            {
                cheap.Add((store.StoreId, total));
            }
            
        }

        if (cheap.Count == 0) return new StoreDto();
        
        
        var cheapestStore = cheap.OrderBy(x => x.total).FirstOrDefault();
        var result = _dbContext.Stores
            .FirstOrDefault(x => x.StoreId == cheapestStore.store);
        
        
        return new StoreDto() { Name = result.Name, Address = result.Address };
        
    }

}