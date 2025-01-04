using AppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }
        public void CreateUser()
        {
            //Create User

            //Register in TXT

            //Register in LogTXT

        }
        public void RetrieveUsers()
        {

        }

        public void UpdateUser()
        {

        }

        public void DeleteUser()
        {

        }
    }
}
