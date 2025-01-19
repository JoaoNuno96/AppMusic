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
        private readonly PathDirectoryService _pathDirectoryService;
        public List<Music> ListOfMusics { get; set; } = new List<Music>();

        public MusicService(PathDirectoryService pds)
        {
            this._pathDirectoryService = pds;
            this.StoreRead();
        }
        public string RepositoryPath
        {
            get
            {
                return this._pathDirectoryService.Paths.RepositoryPath;
            }
        }

        //VERIFY IF MUSIC IS AVAILABLE
        public bool VerifyMusic(Music music)
        {
            if (music.Available == false)
            {
                return false;
            }
            return true;
        }

        //PRINT MUSIC STATUS
        public void VerifyMusicProcess(bool status)
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
        
        //READS THE MUSIC REPOSITORY [STORE]
        public void StoreRead()
        {
            using (StreamReader sr = File.OpenText(this.RepositoryPath))
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
                    ListOfMusics.Add(new Music(musicId, musicName, musicBand, musicPrice, musicUpload, musicAvail));
                }
            }
        }

        //WRITE THE TABLE
        public void StoreTableWrite()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var data = this.InitEmployee();
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

        public DataTable InitEmployee()
        {

            var table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("Band");
            table.Columns.Add("Price");
            table.Columns.Add("Upload Date");
            table.Columns.Add("Available");

            foreach (Music music in ListOfMusics)
            {
                table.Rows.Add(music.Id, music.Name, music.Band, music.Price, music.UploadTime, music.Available);
            }
            return table;
        }

    }
}
