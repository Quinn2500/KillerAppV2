using Business.Dashboard;

namespace Business.Overhoring
{
    public class OverhoringLogic
    {
        public DataModellen.Overhoring Start(string id, string username)
        {
            ListOverviewLogic getLijst = new ListOverviewLogic();
            DataModellen.Overhoring overhoring = new DataModellen.Overhoring();
            string[] optieString = id.Split('%');
            string lijstNaam = optieString[0];
            overhoring.WoordenLijst = getLijst.GiveList(lijstNaam, username).WoordenLijst;
            string[] opties = optieString[1].Split('|');
            overhoring.Vraag = opties[0].Split('?')[0].Split('=')[1];
            overhoring.Soort = opties[1].Split('=')[1];
            bool random;
            if (opties[2].Split('=')[1] == "RandomVologorde")
            {
                random = true;
            }
            else
            {
                random = false;
            }

            overhoring.Random = random;
            return overhoring;
        }
    }
}
