using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreMania.Models
{
    public class Igrac
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public int godiste { get; set; }
        public string pozicija { get; set; }

        public Igrac(int id, string ime, string prezime, int godiste, string pozicija)
        {
            this.id = id;
            this.ime = ime;
            this.prezime = prezime;
            this.godiste = godiste;
            this.pozicija = pozicija;
        }
    }
}
