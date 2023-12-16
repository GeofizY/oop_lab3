using System.ComponentModel.DataAnnotations;

namespace Models;

public class Store
{
    [Key]
    public int StoreId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public List<Product> Products { get; set; } = new();
    public List<ProductInStore> ProductsInStore { get; set; } = new();
}