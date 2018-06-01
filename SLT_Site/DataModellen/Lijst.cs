using System;
using System.Collections.Generic;

namespace DataModellen
{
    public class Lijst
    {
        public string Gebruikersnaam;
        public string Soort;
        public string Datum;
        public int isPublic;
        public string Titel;
        public List<Woord> WoordenLijst = new List<Woord>();
    }
}
