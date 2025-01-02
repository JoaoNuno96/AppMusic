using AppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMusic.Services
{
    public class LogService
    {
        public string Path { get; set; }
        public ICollection<Log> ListLogs { get; set; }
        public LogService()
        {
            this.Path = this.DirectLogPath();
            this.ListLogs = new List<Log>();
        }

        public string DirectLogPath()
        {

        }

        public void ReadLog()
        {

        }

        public void WriteLog(Log log)
        {

        }

    }
}
