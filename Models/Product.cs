using System.ComponentModel.DataAnnotations;

namespace Models;

public class Product
{
    [Key]
    public string Name { get; set; }

    public List<Store> Stores { get; set; } = new();
    public List<ProductInStore> ProductsInStore = new ();
}