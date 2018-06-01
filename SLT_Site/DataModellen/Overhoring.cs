using System;
using System.Collections.Generic;
using System.Text;

namespace DataModellen
{
    public class Overhoring
    {
        private string mSoort;
        private bool mRandom;
        private string mVraag;
        private int mAantalFout;
        private int mAantalGoed;
        private List<Woord> mWoordenLijst;

        public string Soort{get { return mSoort;}set { mSoort = value; }}
        public bool Random { get { return mRandom; } set { mRandom = value; } }
        public string Vraag { get { return mVraag; } set { mVraag = value; } }
        public int AantalFout { get { return mAantalFout; } set { mAantalFout = value; } }
        public int AantalGoed { get { return mAantalGoed; } set { mAantalGoed = value; } }
        public List<Woord> WoordenLijst { get { return mWoordenLijst; } set { mWoordenLijst = value; } }


    }
}
