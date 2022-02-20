﻿namespace EcommerceService.Core.Domain;

public class Order : Base
{
    public int ProductId { get; set; }
    public int CampaignId { get; set; }

    public string ProductCode { get; set; }
    public decimal CurrentPrice { get; set; }
    public int Quantity { get; set; }
}