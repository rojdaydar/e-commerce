using AutoMapper;
using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Order;
using EcommerceService.Core.Exceptions;
using EcommerceService.Core.Repositories;
using EcommerceService.Core.Services;

namespace EcommerceService.Service.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Campaign> _campaingRepository;
    private readonly IMapper _mapper;

    public OrderService(IRepository<Order> orderRepository, IMapper mapper, IRepository<Product> productRepository,
        IRepository<Campaign> campaingRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _campaingRepository = campaingRepository;
    }

    public void Create(CreateOrderInput createOrderInput)
    {
        var order = _mapper.Map<Order>(createOrderInput);

        var product = existProduct(order.ProductCode, order.Quantity);

        var campaign = _campaingRepository.Find(p => p.ProductId == product.Id && !p.IsDeleted);

        if (campaign is null)
            order.CurrentPrice = product.Price;
        else
            order.CurrentPrice = campaign.CurrentProductPrice;

        updatedProductStock(product);

        _orderRepository.Add(order);
        _orderRepository.SaveChanges();
    }

    private Product existProduct(string productCode, int quantity)
    {
        var product = _productRepository.Find(p => p.ProductCode.Equals(productCode) && !p.IsDeleted);

        if (product is null)
            throw new CustomException("Stokta yeterli ürün bulunmamaktadır.", "1000");

        if (product.Stock < quantity)
            throw new CustomException("Stokta yeterli ürün bulunmamaktadır.", "1010");

        product.Stock = -quantity;

        return product;
    }

    private void updatedProductStock(Product product)
    {
        _productRepository.Update(product);
        _productRepository.SaveChanges();
    }
}