using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AppMusic.Entities;
using System.Globalization;
using ConsoleTables;
using System.Data;
using AppMusic.Services.Exceptions;


namespace AppMusic.Services
{
    class MusicService
    {
        public string baseDir = (AppContext.BaseDirectory.Substring(0, 49) + @"\Repository\Store.txt").ToString();
        public List<Music> listOfMusics { get; set; } = new List<Music>();

        public bool verifyMusic(Music music)
        {
            if (music.available == false)
            {
                return false;
            }
            return true;
        }

        public void verifyMusicProcess(bool status)
        {
            if (status)
            {
                Console.WriteLine("Music available");
            }
            else
            {
                throw new MusicNotAvailableException("Music is not available");
            }
        }

        public void storeRead()
        {
            using (StreamReader sr = File.OpenText(baseDir))
            {
                while (!sr.EndOfStream)
                {
                    string[] vect = sr.ReadLine().Split(',');
                    int musicId = int.Parse(vect[0]);
                    string musicName = vect[1];
                    string musicBand = vect[2];
                    double musicPrice = double.Parse(vect[3], CultureInfo.InvariantCulture);
                    DateTime musicUpload = DateTime.Parse(vect[4]);
                    bool musicAvail = bool.Parse(vect[5]);
                    listOfMusics.Add(new Music(musicId, musicName, musicBand, musicPrice, musicUpload, musicAvail));
                }
            }

        }

        public void storeTableWrite()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var data = initEmployee();
            string[] columnNames = data.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

            DataRow[] rows = data.Select();

            var table = new ConsoleTable(columnNames);
            foreach (DataRow row in rows)
            {
                table.AddRow(row.ItemArray);
            }
            table.Write(Format.Default);

        }

        public DataTable initEmployee()
        {

            var table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("Band");
            table.Columns.Add("Price");
            table.Columns.Add("Upload Date");
            table.Columns.Add("Available");

            foreach (Music music in listOfMusics)
            {
                table.Rows.Add(music.id, music.name, music.band, music.price, music.uploadTime, music.available);
            }
            return table;
        }

    }
}
