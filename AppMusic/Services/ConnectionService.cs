using AppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Services
{
    public class ConnectionService
    {
        public LogService LogService { get; set; }
        public string? Path { get; set; }
        public Connection Connection { get; set; }
        public ConnectionService()
        {
            this.LogService = new LogService(new PathDirectoryService());
            this.RecoverApplicationSourcePath();
            this.Connection = this.RecoverConnectionData();
        }

        public Connection RecoverConnectionData()
        {
            try
            {
                using StreamReader reader = File.OpenText(this.Path);
                {
                    List<string> lines = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }

                    return new Connection(lines[0], lines[1], lines[2], lines[3], lines[4]);
                }
            }
            catch (Exception ex)
            {
                this.LogService.WriteLog(new LogError
                {
                    ErroMessage = ex.Message,
                    ErrorCode = ex.Source
                });
                return null;
            }

        }

        private void RecoverApplicationSourcePath()
        {
            try
            {
                string appconfigurationtxt = AppContext.BaseDirectory.ToString();

                this.Path = appconfigurationtxt + @"\Auth\auth.txt";
            }
            catch (Exception ex)
            {
            }

        }

        #region MUSIC

        

        #endregion

        



    }
}
