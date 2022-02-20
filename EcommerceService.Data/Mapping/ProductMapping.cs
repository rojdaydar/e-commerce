using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceService.Data.Mapping;

public  abstract class ProductMapping : BaseMapping<Product>
{
    protected ProductMapping(EntityTypeBuilder<Product> builder) : base(builder)
    {
        builder.Property(p => p.Price).IsRequired().HasPrecision(12, 2);
        builder.Property(p => p.ProductCode).IsRequired();
    }
}