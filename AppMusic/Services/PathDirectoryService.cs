﻿using AppMusic.Entities;
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
        }

        //PROCESS THE RECOVERY OF ALL PATHS EVEN IF THE APPLICATION IS IN DIFFERENT CPU
        public void RecoverPaths(string? name)
        {
            List<string> source = AppContext.BaseDirectory.Split("\\").ToList();
            List<int> binIndex = source.Where(x => x == "bin").Select(x => source.IndexOf(x)).ToList();
            List<string> fragmentedPAth = source.Skip(0).Take(binIndex[0]).ToList();

            String dinamicPath = String.Join("\\", fragmentedPAth);

            this.Paths.AuthPath = dinamicPath + @"\Auth";
            this.Paths.InvoicePath = dinamicPath + @"\Invoice";
            this.Paths.LogPath = dinamicPath + @"\Logs";
            this.Paths.RepositoryPath = dinamicPath + @"\Repository";
            this.Paths.ApplicationDirectoryPath = dinamicPath;
        }
    }
}
