using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceService.Data.Mapping;

public abstract class BaseMapping<T> where T : Base
{
    protected BaseMapping(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).IsRequired();
        builder.Property(p => p.UpdatedDate).IsRequired();
        builder.Property(p => p.IsDeleted).IsRequired();
    }
}