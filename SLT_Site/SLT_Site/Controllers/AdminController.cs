using Business.Dashboard;
using Business.Login;
using DataModellen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLT_Site.Models.Dashboard;
using SLT_Site.Models.Home;

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
            PublicListModel model = new PublicListModel
            {
                PublicLists = logic.GetAllPublicLists(HttpContext.Session.GetString("Username"))
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult ApproveList(string id)
        {
            string[] stringSplit = id.Split('?');
            admin.Approve(stringSplit[0],stringSplit[1]);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RemovePublic(string id)
        {
            string[] stringSplit = id.Split('?');
            admin.RemovePublic(stringSplit[0], stringSplit[1]);
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
            if (!ModelState.IsValid || ac.CheckIfUserExitst(model.Username) != null)
            {
                ViewData["Bericht"] = ac.CheckIfUserExitst(model.Username);
                return View(model);
            }
            Admin user = new Admin()
            {
                Username = model.Username,
                Email = model.Email,
                FirstName = model.Fname,
                LastName = model.Lname,
                Password = model.Password
            };
            ac.RegisterAdmin(user);
            TempData["Succes"] = "Uw account is succesvol aangemaakt.";
            return RedirectToAction("Index");
        }
    }
}