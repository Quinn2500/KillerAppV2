using System.Collections.Generic;

namespace DataModellen
{
    public class Overhoring
    {
        public string Soort { get; set; }
        public bool Random { get; set; }
        public string Vraag { get; set; }
        public int AantalFout { get; set; }
        public int AantalGoed { get; set; }
        public List<Woord> WoordenLijst { get; set; }
    }
}
