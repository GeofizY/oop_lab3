using DAL;
using Models;
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

}