using AppMusic.Entities.Enums;
using AppMusic.Entities;
using AppMusic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Services
{
    class OrderService
    {
        private readonly PathDirectoryService _pathDirectoryService;
        public MusicService MusicService { get; set; }
        public RepositoryService RepositoryService { get; set; }
        public Order Order { get; set; }
        public OrderService(PathDirectoryService pds, MusicService ms, RepositoryService rs,Order o)
        {
            this._pathDirectoryService = pds;
            this.MusicService = ms;
            this.RepositoryService = rs;
            this.Order = o;
        }

        public string InvoicePath
        {
            get
            {
                return this._pathDirectoryService.Paths.InvoicePath;
            }
        }

        public void AddSong(Music M)
        {
            this.Order.OrderList.Add(M);
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
            this.Order.OrderList.Remove(M);
        }

        public void OrderIdIncrement()
        {
            string baseDir = this.InvoicePath;
            //var baseDir = AppContext.BaseDirectory.Substring(0, 45) + @"\Invoice\";
            var i = Directory.GetFiles(baseDir).Length;
            this.Order.OrderId += i;
        }
    }
}
