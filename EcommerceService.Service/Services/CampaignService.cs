using AutoMapper;
using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.Exceptions;
using EcommerceService.Core.Repositories;
using EcommerceService.Core.Services;

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

        if (addedCampaign is {Id: 0})
            throw new Exception();
    }

    public CampaignDto Detail(string name)
    {
        throw new Exception();

    }
}