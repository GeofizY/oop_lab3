using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
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

}