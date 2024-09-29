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
        public MusicService MusicService { get; set; }
        public List<Music> OrderList { get; set; }
        public RepositoryService RepositoryService { get; set; }
        public Buyer BuyerDetails { get; set; }
        public OrderStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public Order(MusicService ms, Buyer bd, string pm)
        {
            this.MusicService = new MusicService();
            this.OrderList = new List<Music>();
            this.RepositoryService = new RepositoryService();

            this.BuyerDetails = bd;
            this.Status++;
            this.MusicService = ms;
            this.PaymentMethod = pm;
        }

        public void AddSong(Music M)
        {
            
            this.OrderList.Add(M);
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
            this.OrderList.Remove(M);
        }

        public void OrderIdIncrement()
        {
            var baseDir = AppContext.BaseDirectory.Substring(0, 49) + @"\Invoice\";
            var i = Directory.GetFiles(baseDir).Length;
            this.OrderId += i;
        }

    }

}
