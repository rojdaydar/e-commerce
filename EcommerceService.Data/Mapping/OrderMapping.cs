using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceService.Data.Mapping;

public abstract class OrderMapping : BaseMapping<Order>
{
    protected OrderMapping(EntityTypeBuilder<Order> builder) : base(builder)
    {
        builder.Property(p => p.CurrentPrice).HasPrecision(12, 2);
        builder.Property(p => p.Quantity).IsRequired();
    }
}