using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceService.Core.Mapping;

public  abstract class ProductMapping : BaseMapping<Product>
{
    protected ProductMapping(EntityTypeBuilder<Product> builder) : base(builder)
    {
      
    }
}