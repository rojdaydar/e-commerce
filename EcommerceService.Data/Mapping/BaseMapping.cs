using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceService.Core.Mapping;

public abstract class BaseMapping<T> where T : Base
{
    protected BaseMapping(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(m => m.Id);
    }
}