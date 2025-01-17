using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Entities
{
    public class Connection
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public string Database { get; set; }

        public Connection(string h, string p, string uid, string upass, string data)
        {
            this.Host = h;
            this.Port = p;
            this.UserId = uid;
            this.UserPassword = upass;
            this.Database = data;
        }

        public string ConnectionString()
        {
            return $"server={this.Host}; port={this.Port}; uid={this.UserId}; pwd={this.UserPassword}; database={this.Database}";
        }

    }
}
