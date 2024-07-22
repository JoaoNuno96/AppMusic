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
        public int orderId { get; set; } = 1;
        public MusicService musicService { get; set; } = new MusicService();
        public List<Music> orderList { get; set; } = new List<Music>();
        public RepositoryService repositoryService { get; set; } = new RepositoryService();
        public Buyer buyerDetails { get; set; }
        public OrderStatus status { get; set; }
        public string paymentMethod { get; set; }
        public Order(MusicService ms, Buyer bd, string pm)
        {
            buyerDetails = bd;
            status++;
            musicService = ms;
            paymentMethod = pm;
        }

        public void addSong(Music M)
        {
            orderList.Add(M);
        }

        public void addSongs(List<Music> L)
        {
            foreach (Music M in L)
            {
                addSong(M);
            }
        }

        public void removeSong(Music M)
        {
            orderList.Remove(M);
        }

        public void orderIdIncrement()
        {
            var baseDir = AppContext.BaseDirectory.Substring(0, 49) + @"\Invoice\";
            var i = Directory.GetFiles(baseDir).Length;
            orderId += i;
        }

    }

}
