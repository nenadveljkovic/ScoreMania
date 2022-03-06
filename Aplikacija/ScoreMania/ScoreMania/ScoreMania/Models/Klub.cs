using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreMania.Models
{
    public class Klub
    {
        public int id { get; set; }
        public string naziv {get;set;}
        public string stadion { get; set; }
        public string trener { get; set; }

        public Klub(int id, string naziv, string stadion, string trener)
        {
            this.id = id;
            this.naziv = naziv;
            this.stadion = stadion;
            this.trener = trener;
        }
    }
}
