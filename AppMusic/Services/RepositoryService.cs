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
        private readonly PathDirectoryService _pathDirectoryService;
        public RepositoryService(PathDirectoryService pd)
        {
            this._pathDirectoryService = pd;
        }
        
        public void RentItemDatabase(int Id)
        {
            List<Music> list = new List<Music>();

            //READ FILE

            using (StreamReader sr = File.OpenText(this._pathDirectoryService.Paths.RepositoryPath))
            {
                while (!sr.EndOfStream)
                {
                    string[] vect = sr.ReadLine().Split(',');

                    int idSong = int.Parse(vect[0]);
                    string name = vect[1];
                    string band = vect[2];
                    double price = double.Parse(vect[3], CultureInfo.InvariantCulture);
                    DateTime date = DateTime.Parse(vect[4]);
                    bool avail = bool.Parse(vect[5]);
                    list.Add(new Music(idSong, name, band, price, date, avail));
                }
            }

            //PROCESS FILE

            var processFile = list.Where(x => x.Id == Id)
                                  .FirstOrDefault();
            processFile.Available = false;

            //RETURN TEXT

            using (StreamWriter sw = new StreamWriter(this._pathDirectoryService.Paths.RepositoryPath,false))
            {
                foreach(Music music in list)
                {
                    string line = $"{music.Id},{music.Name},{music.Band},{music.Price.ToString(CultureInfo.InvariantCulture)},{music.UploadTime.ToString("dd/MM/yyyy")},{music.Available}";
                    sw.WriteLine(line);
                }
            }

        }

    }
}
