using Business.Dashboard;
using Business.Login;
using DataModellen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using SLT_Site.Models;
using SLT_Site.Models.Dashboard;

namespace SLT_Site.Controllers
{
    public class AdminController : Controller
    {
        AdminLogic admin = new AdminLogic();
        public ActionResult Index()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            return View();
        }

        [HttpGet]
        public ActionResult OpenbareLijsten()
        {
            PublicListLogic logic = new PublicListLogic();
            PublicListModel model = new PublicListModel();
            model.PublicLists = logic.GetAllPublicLists(HttpContext.Session.GetString("Username"));
            return View(model);
        }

        [HttpGet]
        public ActionResult ApproveList(string id)
        {
            string[] StringSplit = id.Split('?');
            admin.Approve(StringSplit[0],StringSplit[1]);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RemovePublic(string id)
        {
            string[] StringSplit = id.Split('?');
            admin.RemovePublic(StringSplit[0], StringSplit[1]);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            AccountModel model = new AccountModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAdmin(AccountModel model)
        {
            AccountLogic ac = new AccountLogic();
            if (!ModelState.IsValid || ac.CheckIfUserExitst(model.username) != null)
            {
                ViewData["Bericht"] = ac.CheckIfUserExitst(model.username);
                return View(model);
            }
            Admin user = new Admin()
            {
                Username = model.username,
                Email = model.email,
                FirstName = model.fname,
                LastName = model.lname,
                Password = model.password
            };
            ac.RegisterAdmin(user);
            TempData["Succes"] = "Uw account is succesvol aangemaakt.";
            return RedirectToAction("Index");
        }
    }
}