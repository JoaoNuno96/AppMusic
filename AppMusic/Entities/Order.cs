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
        public int OrderId { get; set; } = 1;
        public MusicService MusicService { get; set; } = new MusicService();
        public List<Music> OrderList { get; set; } = new List<Music>();
        public RepositoryService RepositoryService { get; set; } = new RepositoryService();
        public Buyer BuyerDetails { get; set; }
        public OrderStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public Order(MusicService Ms, Buyer buyerDetails, string paymentMethod)
        {
            BuyerDetails = buyerDetails;
            Status++;
            MusicService = Ms;
            PaymentMethod = paymentMethod;
        }

        public void AddSong(Music M)
        {
            OrderList.Add(M);
        }

        public void AddSongs(List<Music> L)
        {
            foreach (Music M in L)
            {
                AddSong(M);
            }
        }

        public void RemoveSong(Music M)
        {
            OrderList.Remove(M);
        }

        public void OrderIdIncrement()
        {
            var BaseDir = AppContext.BaseDirectory.Substring(0, 49) + @"\Invoice\";
            var i = Directory.GetFiles(BaseDir).Length;
            OrderId += i;
        }

    }

}
