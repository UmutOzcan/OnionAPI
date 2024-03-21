using OnionAPI.Domain.Common;
namespace OnionAPI.Domain.Entities;

// idlere ihtiyac oldugundan ara tablo olustururuz
public class ProductCategory : IEntityBase
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Product Product { get; set; }
    public Category Category { get; set; }
}
