using Business.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLT_Site.Models.Dashboard;

namespace SLT_Site.Controllers
{
    public class AdminController : Controller
    {
        AdminLogic admin = new AdminLogic();
        public ActionResult Index()
        {
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
    }
}