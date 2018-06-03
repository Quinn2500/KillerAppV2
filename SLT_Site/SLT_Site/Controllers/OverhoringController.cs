using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Overhoring;
using DataModellen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SLT_Site.Controllers
{
    public class OverhoringController : Controller
    {
        public ActionResult OptieMenu(string id)
        {
            ViewBag.LijstID = id;
            return View();
        }

        public ActionResult StartOverhoring(string id)
        {
            OverhoringLogic logic = new OverhoringLogic();
            ViewBag.Overhoring = logic.Start(id, HttpContext.Session.GetString("Username"));
            List<string[]> test = new List<string[]>();
            foreach (Woord w in logic.Start(id, HttpContext.Session.GetString("Username")).WoordenLijst)
            {
                string[] s = new string[2];
                s[0] = w.Begrip;
                s[1] = w.Betekenis;
                test.Add(s);
            }

            ViewBag.Test = JsonConvert.SerializeObject(test);
            return View();
        }
    }
}