using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AppMusic.Entities;
using System.Net.Http.Headers;

namespace AppMusic.Services
{
    class RepositoryService
    {
        public string Source { get; set; } = AppContext.BaseDirectory.Substring(0, 49) + @"\Repository\Store.txt" ;

        public RepositoryService() { }
        
        public void RentItemDatabase(int Id)
        {
            List<Music> List = new List<Music>();

            //READ FILE

            using (StreamReader sr = File.OpenText(Source))
            {
                while (!sr.EndOfStream)
                {
                    string[] vect = sr.ReadLine().Split(',');

                    int IdSong = int.Parse(vect[0]);
                    string Name = vect[1];
                    string Band = vect[2];
                    double Price = double.Parse(vect[3], CultureInfo.InvariantCulture);
                    DateTime Date = DateTime.Parse(vect[4]);
                    bool Avail = bool.Parse(vect[5]);
                    List.Add(new Music(IdSong, Name, Band, Price, Date, Avail));
                }
            }

            //PROCESS FILE

            var processFile = List.Where(x => x.Id == Id)
                                  .FirstOrDefault();
            processFile.Available = false;

            //RETURN TEXT

            using (StreamWriter sw = new StreamWriter(Source,false))
            {
                foreach(Music M in List)
                {
                    string line = $"{M.Id},{M.Name},{M.Band},{M.Price.ToString(CultureInfo.InvariantCulture)},{M.UploadTime.ToString("dd/MM/yyyy")},{M.Available}";
                    sw.WriteLine(line);
                }
            }

        }

        public void ReturnItemDatabase(int id)
        {

        }
    }
}
