using DAL;
using Models;
using Services.Interfaces;

namespace Services.Realization;

public class StoreService : IStoreService
{
    private StoreDbContext _dbContext;

    public StoreService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Create(int id, string name, string address)
    {
        var store = new Store { StoreId = id, Name = name, Address = address };
        var newStore = _dbContext.Stores.Add(store);
        _dbContext.SaveChanges();

        return newStore.Entity.StoreId;
    }
    
}