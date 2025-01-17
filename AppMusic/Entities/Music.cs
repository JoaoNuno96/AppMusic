using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Entities
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Band { get; set; }
        public double Price { get; set; }
        public DateTime UploadTime { get; set; }
        public bool Available { get; set; }

        public Music(int i, string n, string b, double p, DateTime ut, bool a)
        {
            this.Id = i;
            this.Name = n;
            this.Band = b;
            this.Price = p;
            this.UploadTime = ut;
            this.Available = a;
        }

        //Metodo de ovveride de ToString() que apresenta uma frase de resumo do produto. 
        public override string ToString()
        {
            return $"{this.Id}, Name: {this.Band} - {this.Name}, Price: {this.Price}";
        }
    }
}
