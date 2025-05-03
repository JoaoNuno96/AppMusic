using AppMusic.Entities;
using AppMusic.Repository.Queries;
using MySql.Data.MySqlClient;
using Mysqlx.Expr;
using System;
using System.Collections.Generic;
using System.Data;
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
                Console.WriteLine("errors " + ex.Message);
            }

        }


        #region MUSIC

        //RECOVER ALL MUSIC
        public List<Music> RecoverAllMusics()
        {
            List<Music>? list = new List<Music>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.Connection.ConnectionString()))
                {
                    conn.Open();
                    MySqlCommand cmmd = new MySqlCommand()
                    {
                        Connection = conn,
                        CommandText = Queries.QUERY_MUSIC_RECOVER_ALL
                    };

                    using (var result = cmmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            Music music = new Music();

                            music.Id = result.GetInt32(result.GetOrdinal("id"));
                            music.Name = result.GetString(result.GetOrdinal("music_name"));
                            music.Band = result.GetString(result.GetOrdinal("music_band"));
                            music.Price = result.GetDouble(result.GetOrdinal("music_price"));
                            music.UploadTime = result.GetDateTime(result.GetOrdinal("music_upload"));
                            music.Available = result.GetBoolean(result.GetOrdinal("music_available"));

                            list.Add(music);
                        }

                        return list;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return list;
            }
        }

        //RECOVER ONE MUSIC
        public Music RecoverSingle(int id)
        {
            Music music = new Music();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.Connection.ConnectionString()))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = conn,
                        CommandText = Queries.QUERY_MUSIC_RECOVER_SINGLE + $"{id}"
                    };

                    using (var result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            music.Id = result.GetInt32(result.GetOrdinal("id"));
                            music.Name = result.GetString(result.GetOrdinal("music_name"));
                            music.Band = result.GetString(result.GetOrdinal("music_band"));
                            music.Price = result.GetDouble(result.GetOrdinal("music_price"));
                            music.UploadTime = result.GetDateTime(result.GetOrdinal("music_upload"));
                            music.Available = result.GetBoolean(result.GetOrdinal("music_available"));
                        }
                    }
                }
                return music;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return music;
            }
        }

        //CHANGE MUSIC AVAILABLE form 1 to 0
        public bool UpdateMusicStatus(Music musicParam)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.Connection.ConnectionString()))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand()
                    {
                        Connection = conn,
                        CommandText = Queries.QUERY_MUSIC_UPDATING + $"{musicParam.Id}"
                    };

                    command.ExecuteNonQuery();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }



        #endregion





    }
}
