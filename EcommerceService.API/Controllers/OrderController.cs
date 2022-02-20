using System.Net;
using EcommerceService.Core.DTOs.Base;
using EcommerceService.Core.DTOs.Order;
using EcommerceService.Core.DTOs.Product;
using EcommerceService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceService.API.Controllers;

[Route("api/v1/orders")]
[ApiController]
public class OrderController :ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost]
    public ActionResult Create(CreateOrderInput createOrderInput)
    {
        _orderService.Create(createOrderInput);
        return new ObjectResult(null)
        {
            StatusCode = (int) HttpStatusCode.Created
        };
    }

}