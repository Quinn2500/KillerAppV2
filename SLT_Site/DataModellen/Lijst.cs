using System.Collections.Generic;

namespace DataModellen
{
    public class Lijst
    {
        public string Gebruikersnaam { get; set; }
        public string Soort { get; set; }
        public string Datum { get; set; }
        public int IsPublic { get; set; }
        public string Titel { get; set; }
        public List<Woord> WoordenLijst { get; set; }
}
}
