 using Business.Dashboard;
 using Business.Login;
 using Business.Overhoring;
 using DataModellen;
 using DAL;
 using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTesten
    {

        private TestDatabaseCalls db = new TestDatabaseCalls();

        public void CreateUser()
        {
            AccountLogic logic = new AccountLogic();
            logic.RegisterUser(new Gebruiker()
            {
                Email = "quinn@gmail.com",
                Username = "Quinn2500",
                FirstName = "Quinn",
                LastName = "van Veen",
                Password = "@Appel1"
            });
        }

        public void CreateAdmin()
        {
            AccountLogic logic = new AccountLogic();
            logic.RegisterAdmin(new Admin()
            {
                Email = "quinn@gmail.com",
                Username = "DeAdmin",
                FirstName = "Quinn",
                LastName = "van Veen",
                Password = "@Appel1"
            });
        }

        public void DeleteList(string listname, string user)
        {
            DashboardLogic logic = new DashboardLogic();
            logic.DeleteList(listname, user);
        }

        public void Makelist()
        {
            CreateListLogic logic = new CreateListLogic();
            logic.CreateList("De Titel/Woordje1-Woordje2|Gras-Groen|?isPublic=true/Woordjes", "Quinn2500");
        }

        [TestMethod]
        public void TestRegister()
        {
            AccountLogic logic = new AccountLogic();
            logic.RegisterUser(new Gebruiker()
            {
                Email = "quinn@gmail.com",
                Username = "Quinn2500",
                FirstName = "Quinn",
                LastName = "van Veen",
                Password = "@Appel1"
            });
            Assert.IsNotNull(logic.CheckIfUserExitst("Quinn2500"), "De gèbruiker is niet aangemaakt");
            db.DeleteUser("Quinn2500");

        }

        [TestMethod]
        public void LoginUser()
        {
            LoginLogic logic = new LoginLogic();
            CreateUser();
            Assert.IsNotNull(logic.Login("Quinn2500", "@Appel1"), "Kan gebruiker niet inloggen");
            Assert.IsNull(logic.Login("Quinn2500", "@Apel1"), "Kan gebruiker inloggen vanwege verkeer wachtwoord");
            db.DeleteUser("Quinn2500");
        }

        [TestMethod]
        public void CheckIfAdmin()
        {
            LoginLogic logic = new LoginLogic();
            CreateUser();
            CreateAdmin();
            Assert.IsTrue(logic.CheckIfAdmin("DeAdmin"), "Gebruiker bestaat niet");
            Assert.IsFalse(logic.CheckIfAdmin("Quinn2500"), "Gebruiker bestaat niet");
            db.DeleteUser("Quinn2500");
            db.DeleteUser("DeAdmin");
        }

        [TestMethod]
        public void CreateList()
        {
            CreateListLogic logic = new CreateListLogic();
            CreateUser();
            Assert.IsNotNull(logic.CreateList("De Titel/Woordje1-Woordje2|Gras-Groen|?isPublic=false/Woordjes", "Quinn2500"),"Lijst is niet aangemaakt");
            db.DeleteUser("Quinn2500");
            DeleteList("De Titel", "Quinn2500");
        }

        [TestMethod]
        public void DashboardListOverview()
        {
            ListOverviewLogic logic = new ListOverviewLogic();
            CreateUser();
            Makelist();
            Assert.IsNotNull(logic.GiveList("De Titel", "Quinn2500"), "Kan lijst niet ophalen");
            db.DeleteUser("Quinn2500");
            DeleteList("De Titel", "Quinn2500");
        }

        [TestMethod]
        public void PublicList()
        {
            PublicListLogic logic = new PublicListLogic();
            Makelist();
            Assert.IsNotNull(logic.GetAllPublicLists("Quinn"), "Kan lijsten niet ophalen");
            DeleteList("De Titel", "Quinn2500");
        }

        [TestMethod]
        public void ApprovedList()
        {
            PublicListLogic logic = new PublicListLogic();
            Makelist();
            Assert.IsNotNull(logic.GetAllApprovedLists("Quinn"), "Kan lijsten niet ophalen");
            DeleteList("De Titel", "Quinn2500");
        }

        [TestMethod]
        public void StartOverhoring()
        {
            OverhoringLogic logic = new OverhoringLogic();
            Makelist();
            Assert.IsNotNull(logic.Start("De Titel%Vraag=Begrip?Antwoord=Betekenis|Soort=Standaard|Optie=StandaardVologorde|", "Quinn2500"), "Kan overhoring niet starten");
            DeleteList("De Titel", "Quinn2500");
        }
    }
}
