using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreMania.Models
{
    public class Utakmica
    {
        public int id { get; set; }
        public string datum { get; set; }
        public string dgolovi { get; set; }
        public string ggolovi { get; set; }
        public string sudija { get; set; }
        public string vreme { get; set; }

        public Utakmica(int id, string datum, string dgolovi, string ggolovi, string sudija, string vreme)
        {
            this.id = id;
            this.datum = datum;
            this.dgolovi = dgolovi;
            this.ggolovi = ggolovi;
            this.sudija = sudija;
            this.vreme = vreme;
        }
    }
}
