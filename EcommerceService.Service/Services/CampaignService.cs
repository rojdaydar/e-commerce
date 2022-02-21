using AutoMapper;
using DateTimeProviders;
using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.Exceptions;
using EcommerceService.Core.Extensions;
using EcommerceService.Core.Repositories;
using EcommerceService.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace EcommerceService.Service.Services;

public class CampaignService : ICampaignService
{
    private readonly IRepository<Campaign> _campaignRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CampaignService(IRepository<Campaign> campaignRepository, IMapper mapper,
        IRepository<Product> productRepository)
    {
        _campaignRepository = campaignRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public void Create(CreateCampaignInput createCampaignInput)
    {
        var ss = DateTimeProvider.Now;
        var campaign = _mapper.Map<Campaign>(createCampaignInput);

        var product = _productRepository.Find(p => p.ProductCode.Equals(campaign.ProductCode) && !p.IsDeleted);

        if (product is null)
            throw new CustomException("Kampanyalı ürün bulunamadı.", "3000");

        campaign.ProductId = product.Id;
        _campaignRepository.Add(campaign);
        _campaignRepository.SaveChanges();
    }

    public CampaignDto? Detail(string name)
    {
        var campaign = _campaignRepository.Query().Include(c => c.Product)
            .Include(c => c.Orders).FirstOrDefault(c => c.Name.Equals(name) && !c.IsDeleted);
        if (campaign is null)
            return null;

        var orders = campaign.Orders.ToList();
        CampaignDto campaignDto = new()
        {
            Status = !campaign.IsDeleted,
            TargetSales = campaign.TargetSalesCount,
            TotalSales = calculateTotalSales(orders),
            AvarageItemPrice = calculateAvarageItemPrice(orders)
        };

        return campaignDto;
    }

    private int calculateTotalSales(List<Order> orders)
    {
        if (orders.Count is 0)
            return default;
        return orders.Select(o => o.Quantity).Sum();
    }

    private decimal calculateAvarageItemPrice(List<Order> orders)
    {
        if (orders.Count is 0)
            return default;
        return orders.Select(o => o.CurrentPrice).Average();
    }

    public async Task CheckCampaingJob()
    {
        var campaigns = _campaignRepository.Query().Include(e => e.Product).Include(d => d.Orders).ToList();

        DateTime currentDate = DateTime.Now;
        foreach (var campaign in campaigns)
        {
            DateTime campaignExpireDate = campaign.CreatedDate.AddHours(campaign.Duration);
            if (currentDate <= campaignExpireDate)
            {
                _campaignRepository.Delete(campaign);
                await _campaignRepository.SaveChangesAsync();
                return;
            }

            long totalSoldProduct = campaign.Orders.Select(o => o.Quantity).Sum();
            long totalProductStock = totalSoldProduct + campaign.Product.Stock;

            for (int v = 5; v <= 100; v += 5)
            {
                double perRate = (totalProductStock * v) / 100;

                double targetproduProductStock = totalProductStock - perRate;
                
                if(targetproduProductStock<totalSoldProduct)
                {
                    decimal currentProductPrice;
                    currentProductPrice = campaign.CurrentProductPrice- campaign.PriceManipulationLimit;
                    campaign.CurrentProductPrice = currentProductPrice;
                    
                    _campaignRepository.Update(campaign);
                    await _campaignRepository.SaveChangesAsync();
                    return;
                }
            }
        }
    }
}