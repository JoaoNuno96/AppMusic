using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppMusic.Services;
using AppMusic.Entities.Enums;
using AppMusic.Services.Exceptions;

namespace AppMusic.Entities
{
    class Order
    {
        public int OrderId { get; set; } = 0;
        public MusicService MusicService { get; set; } = new MusicService();
        public Buyer BuyerDetails { get; set; }
        public OrderStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public Order(MusicService Ms, Buyer buyerDetails, string paymentMethod)
        {
            OrderId++;
            BuyerDetails = buyerDetails;
            Status++;
            MusicService = Ms;
            PaymentMethod = paymentMethod;
        }

    }
}
