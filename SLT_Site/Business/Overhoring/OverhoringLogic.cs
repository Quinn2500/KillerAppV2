using System;
using System.Collections.Generic;
using System.Text;
using Business.Dashboard;

namespace Business.Overhoring
{
    public class OverhoringLogic
    {
        public DataModellen.Overhoring Start(string id, string username)
        {
            ListOverviewLogic getLijst = new ListOverviewLogic();
            DataModellen.Overhoring overhoring = new DataModellen.Overhoring();
            string[] OptieString = id.Split('%');
            string LijstNaam = OptieString[0];
            overhoring.WoordenLijst = getLijst.GeefLijst(LijstNaam, username).WoordenLijst;
            string[] Opties = OptieString[1].Split('|');
            overhoring.Vraag = Opties[0].Split('?')[0].Split('=')[1];
            overhoring.Soort = Opties[1].Split('=')[1];
            bool Random;
            if (Opties[2].Split('=')[1] == "RandomVologorde")
            {
                Random = true;
            }
            else
            {
                Random = false;
            }

            overhoring.Random = Random;
            return overhoring;
        }
    }
}
