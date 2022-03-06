using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreMania.Models
{
    public class Korisnik
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }

        public Korisnik(int id, string email, string password, string username)
        {
            this.id = id;
            this.email = email;
            this.password = password;
            this.username = username;
        }
    }
}
