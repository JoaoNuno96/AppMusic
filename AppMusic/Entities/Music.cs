using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Entities
{
    class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Band { get; set; }
        public double Price { get; set; }
        public DateTime UploadTime { get; set; }
        public bool Available { get; set; }

        public Music(int id, string name, string band, double price, DateTime uploadTime, bool Avail)
        {
            Id = id;
            Name = name;
            Band = band;
            Price = price;
            UploadTime = uploadTime;
            Available = Avail;
        }

        public override string ToString()
        {
            return $"{Id}, Name: {Band} - {Name}, Price: {Price}";
        }
    }
}
