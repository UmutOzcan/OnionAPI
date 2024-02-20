using OnionAPI.Domain.Common;

namespace OnionAPI.Domain.Entities;

public class Product : EntityBase
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int BrandId { get; set; }
    public required decimal Price { get; set; }
    public required decimal Discount { get; set; }
    public Brand Brand { get; set; } // OneToMany
    public ICollection<Category> Categories { get; set; } // ManyToMany - ara tablo olusturmadan

    // public required string ImagePath { get; set; }
}
