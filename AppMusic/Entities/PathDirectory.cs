using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Entities
{
    public class PathDirectory
    {
        private string _authPath;
        private string _invoicePath;
        private string _logPath;
        private string _reporistoryPath;
        private string _applicationDirectoryPath;

        public PathDirectory(){ }
        
        //Protective Properties Layer
        public string AuthPath
        {
            get
            {
                return this._authPath;
            }
            set
            {
                if(value != null)
                {
                    this._authPath = value;
                }
            }
        }

        public string InvoicePath
        {
            get
            {
                return this._invoicePath;
            }
            set
            {
                if (value != null)
                {
                    this._invoicePath = value;
                }
            }
        }

        public string LogPath
        {
            get
            {
                return this._logPath;
            }
            set
            {
                if (value != null)
                {
                    this._logPath = value;
                }
            }
        }

        public string RepositoryPath
        {
            get
            {
                return this._reporistoryPath;
            }
            set
            {
                if (value != null)
                {
                    this._reporistoryPath = value;
                }
            }
        }

        public string ApplicationDirectoryPath
        {
            get
            {
                return this._applicationDirectoryPath;
            }
            set
            {
                if(value != null)
                {
                    this._applicationDirectoryPath = value;
                }
            }
        }

    }
}
