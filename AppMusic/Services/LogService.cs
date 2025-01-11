using AppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public string DirectLogPath()
        {
            return String.Empty;

        }

        public void ReadLog()
        {

        }

        public void WriteLog(Log log)
        {

        }

    }
}
