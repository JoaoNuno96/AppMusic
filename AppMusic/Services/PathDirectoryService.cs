using AppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Services
{
    public class PathDirectoryService
    {
        public PathDirectory Paths { get; set; }

        public PathDirectoryService()
        {
            Paths = new PathDirectory();
            this.RecoverPaths();
        }

        //PROCESS THE RECOVERY OF ALL PATHS EVEN IF THE APPLICATION IS IN DIFFERENT CPU
        public void RecoverPaths()
        {
            string source = AppContext.BaseDirectory.ToString();

            this.Paths.AuthPath = source + @"\Auth\DataAuth.txt";
            this.Paths.InvoicePath = source + @"\Invoice";
            this.Paths.LogPath = source + @"\Logs\Logs.txt";
            this.Paths.LogErrorPath = source + @"\Logs\ErrorLogs.txt";
            this.Paths.RepositoryPath = source + @"\Repository\Store.txt";
            this.Paths.ApplicationDirectoryPath = source;
        }
    }
}
