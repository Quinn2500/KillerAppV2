using Business.Login;
using DataModellen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLT_Site.Models;

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
                    return StatusCode(404, loginLogic.Connectie());
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
                if (loginLogic.Login(model.username, model.password) != null)
                {
                    HttpContext.Session.SetString("Username", model.username);
                    if (loginLogic.CheckIfAdmin(model.username))
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
            if (!ModelState.IsValid || ac.CheckIfUserExitst(model.username) != null)
            {
                ViewData["Bericht"]= ac.CheckIfUserExitst(model.username);
                return View(model);
            }
            Gebruiker user = new Gebruiker()
            {
                Username = model.username,
                Email = model.email,
                FirstName = model.fname,
                LastName = model.lname,
                Password = model.password
            };
            ac.RegisterUser(user);
            TempData["Succes"] = "Uw account is succesvol aangemaakt.";
            return RedirectToAction("Index");
        }


    }
}
