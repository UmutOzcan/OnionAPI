using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionAPI.Domain.Entities;

namespace OnionAPI.Persistence.Configurations;

// bu Configure ile ortak tabloyu oluştururuz.
public class ProductCategoryConfiguraiton : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(x => new { x.ProductId, x.CategoryId });

        builder.HasOne(p => p.Product)
            .WithMany(pc => pc.ProductCategories)
            .HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);// silinince bağlı yerler de silinir

        builder.HasOne(c => c.Category)
            .WithMany(pc => pc.ProductCategories)
            .HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Cascade);
    }
}
