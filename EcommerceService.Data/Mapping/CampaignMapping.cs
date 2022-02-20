using EcommerceService.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceService.Data.Mapping;

public abstract class CampaignMapping: BaseMapping<Campaign>
{
    protected CampaignMapping(EntityTypeBuilder<Campaign> builder) : base(builder)
    {
        builder.Property(p => p.PriceManipulationLimit).HasPrecision(12, 2);
        builder.Property(p => p.CurrentProductPrice).HasPrecision(12, 2);
    }
}