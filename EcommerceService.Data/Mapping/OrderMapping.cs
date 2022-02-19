using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceService.Core.Mapping;

public abstract class OrderMapping : BaseMapping<Order>
{
    protected OrderMapping(EntityTypeBuilder<Order> builder) : base(builder)
    {
    }
}