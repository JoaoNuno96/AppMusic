using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Entities
{
    class Music
    {
        public int id { get; set; }
        public string name { get; set; }
        public string band { get; set; }
        public double price { get; set; }
        public DateTime uploadTime { get; set; }
        public bool available { get; set; }

        public Music(int i, string n, string b, double p, DateTime ut, bool a)
        {
            id = i;
            name = n;
            band = b;
            price = p;
            uploadTime = ut;
            available = a;
        }

        //Metodo de ovveride de ToString() que apresenta uma frase de resumo do produto. 
        public override string ToString()
        {
            return $"{id}, Name: {band} - {name}, Price: {price}";
        }
    }
}
