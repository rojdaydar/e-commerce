using EcommerceService.Core.DTOs.Order;

namespace EcommerceService.Core.Services;

public interface IOrderService
{
    void Create(CreateOrderInput createOrderInput);
}