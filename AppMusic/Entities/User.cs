using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Entities
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public User()
        { 
        }
        public User(string uN, string em, string p, string pc )
        {
            this.UserName = uN;
            this.Email = em;
            this.Password = p;
            this.ConfirmationPassword = pc;
        }
        public override string ToString()
        {
            return String.Join("/", [this.UserName, this.Email, this.Password, this.ConfirmationPassword]);
        }

    }
}
