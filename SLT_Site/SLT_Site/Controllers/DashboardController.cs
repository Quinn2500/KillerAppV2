using Business.Dashboard;
using DataModellen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLT_Site.Models.Dashboard;

namespace SLT_Site.Controllers
{
    public class DashboardController : Controller
    {
        DashboardLogic mDashboardLogic = new DashboardLogic();

        [HttpGet]
        public ActionResult Index()
        {
            DashboardModel model = new DashboardModel
            {
                Lijsten = mDashboardLogic.ListNames(HttpContext.Session.GetString("Username"))
            };
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
            ListOverviewModel model = new ListOverviewModel();
            Lijst l = logic.CreateList(id, HttpContext.Session.GetString("Username"));
            model.Lijstje = l;
            return View(model);
        }

        [HttpGet]
        public ActionResult ZieLijst(string id)
        {
            ListOverviewModel model = new ListOverviewModel();
            ListOverviewLogic logic = new ListOverviewLogic();
            if (id.Contains("?"))
            {
                string[] t = id.Split('?');
                string user = t[1];
                id = t[0];
                Lijst l = logic.GiveList(id, user);
                model.Lijstje = l;
            }
            else
            {
                Lijst l = logic.GiveList(id, HttpContext.Session.GetString("Username"));
                model.Lijstje = l;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult OpenbareLijsten()
        {
            PublicListLogic logic = new PublicListLogic();
            PublicListModel model = new PublicListModel
            {
                PublicLists = logic.GetAllPublicLists(HttpContext.Session.GetString("Username")),
                ApprovedLists = logic.GetAllApprovedLists(HttpContext.Session.GetString("Username"))
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteList(string id)
        {
            mDashboardLogic.DeleteList(id, HttpContext.Session.GetString("Username"));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        public JsonResult CheckTitel(string id)
        {
            CreateListLogic logic = new CreateListLogic();
            return new JsonResult(logic.CheckIfTitelExists(id));     
        }
    }
}