using AppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppMusic.Services
{
    public class LogService
    {
        private readonly PathDirectoryService _pathDirectoryService;
        public ICollection<Log> ListLogs { get; set; }
        public LogService(PathDirectoryService pds)
        {
            this.ListLogs = new List<Log>();
            this._pathDirectoryService = pds;
        }

        public string LogPath
        {
            get
            {
                return this._pathDirectoryService.Paths.LogPath;
            }
            private set
            {

            }
        }

        public string LogErrorPath
        {
            get
            {
                return this._pathDirectoryService.Paths.LogErrorPath;
            }
            private set
            {

            }
        }

        //READS ALL LOGS
        public void ReadLog()
        {
            using (StreamReader sr = File.OpenText(this.LogPath))
            {
                while(!sr.EndOfStream)
                {
                    Log log = new Log
                    {
                        Message = sr.ReadToEnd()
                    };

                    this.ListLogs.Add(log);
                    
                }
            }
        }

        //WRITE IN EACH LOG DEPENDING OF TYPE OF OBJECT
        public void WriteLog(LogBase log)
        {
            if(log is Log)
            {
                using (FileStream fs = new FileStream(this.LogPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(log.ToString());
                }
            }
            else if(log is LogError)
            {
                using (FileStream fs = new FileStream(this.LogErrorPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(log.ToString());
                }
            }
            
        }


    }
}
