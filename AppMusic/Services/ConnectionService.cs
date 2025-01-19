using AppMusic.Entities;
using MySql.Data.MySqlClient;
using Mysqlx.Expr;
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

        //RECOVER CONNECTION DETAILS FROM .TXT
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

        //CHANGE MUSIC AVAILABLE form 1 to 0
        public async Task AddMusic(string tableName, Music music)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.Connection.ConnectionString()))
                {
                    await conn.OpenAsync();
                    MySqlCommand command = new MySqlCommand()
                    {
                        Connection = conn,
                        CommandText = $"UPDATE `{tableName}` SET music_available = 0 WHERE id = {music.Id}"
                    };

                    await command.ExecuteNonQueryAsync();
                }

            }
            catch (Exception ex)
            {
            }
        }



        #endregion





    }
}
