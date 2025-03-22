using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserModel : BaseClass
    {
        private string Username { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }

        public UserModel(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
