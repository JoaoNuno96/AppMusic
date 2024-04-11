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


namespace AppMusic.Services
{
    class MusicService
    {
        public string BaseDir = (AppContext.BaseDirectory.Substring(0, 49) + @"\Repository\Store.txt").ToString();
        public List<Music> ListOfMusics { get; set; } = new List<Music>();

        public void StoreRead()
        {
            using (StreamReader sr = File.OpenText(BaseDir))
            {
                while (!sr.EndOfStream)
                {
                    string[] Vect = sr.ReadLine().Split(',');
                    int M_Id = int.Parse(Vect[0]);
                    string M_Name = Vect[1];
                    string M_Band = Vect[2];
                    double M_Price = double.Parse(Vect[3], CultureInfo.InvariantCulture);
                    DateTime M_Upload = DateTime.ParseExact(Vect[4], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    bool M_Avail = bool.Parse(Vect[5]);
                    ListOfMusics.Add(new Music(M_Id, M_Name, M_Band, M_Price, M_Upload, M_Avail));
                }
            }

        }

        public void StoreTableWrite()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var data = InitEmployee();
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

            foreach (Music M in ListOfMusics)
            {
                table.Rows.Add(M.Id, M.Name, M.Band, M.Price, M.UploadTime, M.Available);
            }
            return table;
        }

    }
}
