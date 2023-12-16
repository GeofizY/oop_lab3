using System.ComponentModel.DataAnnotations;

namespace Models;

public class ProductInStore
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public int StoreId { get; set; }
    public Store? Store { get; set; }
    
    public string ProductName { get; set; }
    public Product? Product { get; set; }
}