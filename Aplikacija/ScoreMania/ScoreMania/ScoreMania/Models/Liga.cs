using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreMania.Models
{
    public class Liga
    {
        public int id { get; set; }
        public string drzava { get; set; }
        public string naziv { get; set; }

        public Liga(int id, string drzava, string naziv)
        {
            this.id = id;
            this.drzava = drzava;
            this.naziv = naziv;
        }
    }
}
