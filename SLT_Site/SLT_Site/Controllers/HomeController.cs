using Business.Login;
using DataModellen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLT_Site.Models.Home;

namespace SLT_Site.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                LoginLogic loginLogic = new LoginLogic();
                if (loginLogic.Connectie() != null)
                {
                    return StatusCode(403, loginLogic.Connectie());
                }
                IndexModel model = new IndexModel();
                ViewData["Message"] = TempData["Succes"];
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                LoginLogic loginLogic = new LoginLogic();
                if (loginLogic.Login(model.Username, model.Password) != null)
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    if (loginLogic.CheckIfAdmin(model.Username))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    TempData["Succes"] = "U bent niet ingelogd";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public ActionResult Account()
        {
            AccountModel model = new AccountModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Account(AccountModel model)
        {
            AccountLogic ac = new AccountLogic();
            if (!ModelState.IsValid || ac.CheckIfUserExitst(model.Username) != null)
            {
                ViewData["Bericht"]= ac.CheckIfUserExitst(model.Username);
                return View(model);
            }
            Gebruiker user = new Gebruiker()
            {
                Username = model.Username,
                Email = model.Email,
                FirstName = model.Fname,
                LastName = model.Lname,
                Password = model.Password
            };
            ac.RegisterUser(user);
            TempData["Succes"] = "Uw account is succesvol aangemaakt.";
            return RedirectToAction("Index");
        }


    }
}
