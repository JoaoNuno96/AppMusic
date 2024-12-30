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

            this.ConfirmUserPassword();
        }
        public void ConfirmUserPassword()
        {
            try
            {
                if (this.Password.GetHashCode() == this.ConfirmationPassword.GetHashCode())
                {
                }
                else
                {
                    throw new Exception("Password do not match the Confirmation Password");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
