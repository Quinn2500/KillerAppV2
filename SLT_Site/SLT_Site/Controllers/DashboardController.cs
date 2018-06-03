using Business;
using Business.Dashboard;
using DataModellen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLT_Site.Models.Dashboard;

namespace SLT_Site.Controllers
{
    public class DashboardController : Controller
    {
        DashboardLogic logic = new DashboardLogic();

        [HttpGet]
        public ActionResult Index()
        {
            DashboardModel model = new DashboardModel();
            model.Lijsten = logic.LijstNamen(HttpContext.Session.GetString("Username"));
            ViewData["User"] = HttpContext.Session.GetString("Username");
            return View(model);
        }

        [HttpGet]
        public ActionResult LijstMaken()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LijstOverzicht(string id)
        {
            CreateListLogic logic = new CreateListLogic();
            Lijst l = logic.CreateList(id, HttpContext.Session.GetString("Username"));
            ViewBag.Lijst = l;
            return View();
        }

        [HttpGet]
        public ActionResult ZieLijst(string id)
        {
            ListOverviewLogic logic = new ListOverviewLogic();
            if (id.Contains("?"))
            {
                string[] t = id.Split('?');
                string user = t[1];
                id = t[0];
                Lijst l = logic.GeefLijst(id, user);
                ViewBag.List = l;
            }
            else
            {
                Lijst l = logic.GeefLijst(id, HttpContext.Session.GetString("Username"));
                ViewBag.List = l;
            }
            return View();
        }

        [HttpGet]
        public ActionResult OpenbareLijsten()
        {
            PublicListLogic logic = new PublicListLogic();
            PublicListModel model = new PublicListModel();
            model.PublicLists = logic.GetAlleOpenbareLijsten(HttpContext.Session.GetString("Username"));
            return View(model);
        }
    }
}