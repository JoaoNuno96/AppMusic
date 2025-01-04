using System;
using System.Collections.Generic;
using AppMusic.Services;
using AppMusic.Entities.Enums;
using AppMusic.Services.Exceptions;

namespace AppMusic.Entities
{
    class Order
    {
        public int OrderId { get; set; } = 1;
        public List<Music> OrderList { get; set; }
        public Buyer BuyerDetails { get; set; }
        public OrderStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public Order(Buyer bd, string pm)
        {
            this.OrderList = new List<Music>();

            this.BuyerDetails = bd;
            this.Status++;
            this.PaymentMethod = pm;
        }

    }

}
