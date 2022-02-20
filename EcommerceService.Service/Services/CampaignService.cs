using AutoMapper;
using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.Exceptions;
using EcommerceService.Core.Repositories;
using EcommerceService.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace EcommerceService.Service.Services;

public class CampaignService : ICampaignService
{
    private readonly IRepository<Campaign> _campaignRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IMapper _mapper;

    public CampaignService(IRepository<Campaign> campaignRepository, IMapper mapper,
        IRepository<Product> productRepository, IRepository<Order> orderRepository)
    {
        _campaignRepository = campaignRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public void Create(CreateCampaignInput createCampaignInput)
    {
        var campaign = _mapper.Map<Campaign>(createCampaignInput);

        var product = _productRepository.Find(p => p.ProductCode.Equals(campaign.ProductCode) && !p.IsDeleted);

        if (product is null)
            throw new CustomException("Kampanyalı ürün bulunamadı.", "3000");

        campaign.ProductId = product.Id;
        var addedCampaign = _campaignRepository.Add(campaign);
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
    
    public async Task CampaingJob()
    {
        var campaigns = _campaignRepository.Query().Include(e => e.Product).Include(d => d.Orders).ToList();

        foreach (var campaign in campaigns)
        {
            long totalSoldProduct = campaign.Orders.Select(o => o.Quantity).Sum();

            long totalProductStock = totalSoldProduct + campaign.Product.Stock;

            DateTime currentDate = DateTime.Now;
            DateTime campaignExpireDate = campaign.CreatedDate.AddHours(campaign.Duration);

            if (currentDate <= campaignExpireDate)
            {
                _campaignRepository.Delete(campaign);
                await _campaignRepository.SaveChangesAsync();
                return;
            }

            TimeSpan gab = campaignExpireDate - currentDate;

            double gabTotalHours = gab.TotalHours;

            double newGabPrice = (double) campaign.PriceManipulationLimit * gabTotalHours;

            campaign.CurrentProductPrice = (decimal) ((double) campaign.Product.Price - newGabPrice);
        }
    }
}