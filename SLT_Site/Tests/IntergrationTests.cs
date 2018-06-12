using System;
using System.Collections.Generic;
using System.Text;
using Business.Dashboard;
using Business.Login;
using Business.Overhoring;
using DataModellen;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class IntergrationTests
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
        public void AccountLogic()
        {
            LoginLogic loginLogic = new LoginLogic();
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
            Assert.IsNotNull(loginLogic.Login("Quinn2500", "@Appel1"), "Kan gebruiker niet inloggen");
            Assert.IsNull(loginLogic.Login("Quinn2500", "@Apel1"), "Kan gebruiker inloggen vanwege verkeer wachtwoord");
            CreateAdmin();
            Assert.IsTrue(loginLogic.CheckIfAdmin("DeAdmin"), "Gebruiker bestaat niet");
            Assert.IsFalse(loginLogic.CheckIfAdmin("Quinn2500"), "Gebruiker bestaat niet");
            db.DeleteUser("Quinn2500");
            db.DeleteUser("DeAdmin");
        }

        [TestMethod]
        public void DashboardLogic()
        {
            CreateListLogic CreateListLogic = new CreateListLogic();
            ListOverviewLogic ListOverviewLogic = new ListOverviewLogic();
            PublicListLogic PublicListlogic = new PublicListLogic();
            CreateUser();
            Makelist();
            Assert.IsNotNull(CreateListLogic.CreateList("De Titel/Woordje1-Woordje2|Gras-Groen|?isPublic=false/Woordjes", "Quinn2500"));
            Assert.IsNotNull(ListOverviewLogic.GiveList("De Titel", "Quinn2500"), "Kan lijst niet ophalen");
            Assert.IsNotNull(PublicListlogic.GetAllPublicLists("Quinn"), "Kan lijsten niet ophalen");
            Assert.IsNotNull(PublicListlogic.GetAllApprovedLists("Quinn"), "Kan lijsten niet ophalen");
            db.DeleteUser("Quinn2500");
            DeleteList("De Titel", "Quinn2500");
        }

        [TestMethod]
        public void OverhoringLogic()
        {
            OverhoringLogic logic = new OverhoringLogic();
            CreateUser();
            Makelist();
            Assert.IsNotNull(logic.Start("De Titel%Vraag=Begrip?Antwoord=Betekenis|Soort=Standaard|Optie=StandaardVologorde|", "Quinn2500"), "Kan overhoring niet starten");
            db.DeleteUser("Quinn2500");
            DeleteList("De Titel", "Quinn2500");
        }
    }
}
