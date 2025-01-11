using AppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AppMusic.Services
{
    public class UserService
    {
        private readonly PathDirectoryService _pathDirectoryService;
        public List<User> ListCreatedUsers { get; set; }

        public UserService(PathDirectoryService pd)
        {
            this._pathDirectoryService = pd;
            this.RetrieveUsers();
        }

        public string AuthPath
        {
            get
            {
                return this._pathDirectoryService.Paths.AuthPath;
            }
        }
        public void CreateUser(User user)
        {
            if(!this.VerifyUsers(user.Email))
            {
                //ADD TO LIST
                this.ListCreatedUsers.Add(user);

                //Register in TXT
                using (FileStream fs = new FileStream(this.AuthPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(user.ToString());
                }
            }
            else
            {
                Console.WriteLine("User  Null");
            }

        }

        public bool VerifyUsers(string emailParam)
        {
            bool state = false;
            string[] usersRegisteredInFile = File.ReadAllLines(this.AuthPath); 

            if(usersRegisteredInFile != null)
            {
                foreach (string userR in usersRegisteredInFile)
                {
                    if (userR.Split("/")[1] == emailParam)
                    {
                        state = true;
                        return state;
                    }
                }
            }
            return state;

        }

        public void RetrieveUsers()
        {
            using (StreamReader sr = File.OpenText(this.AuthPath))
            {
                this.ListCreatedUsers.Add(new User(sr.ReadLine().Split("/")[0], sr.ReadLine().Split("/")[1], sr.ReadLine().Split("/")[2], sr.ReadLine().Split("/")[3]));
            }
        }

        public User SelectUserByEmail(string email)
        {
            return this.ListCreatedUsers
                .FirstOrDefault(x => x.Email == email);

        }

        public void DeleteUser(string email)
        {
            this.ListCreatedUsers.Remove(this.ListCreatedUsers.FirstOrDefault(x => x.Email == email));
        }
    }
}
