using System.Collections.Generic;
using Business.Overhoring;
using DataModellen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SLT_Site.Models.Overhoren;

namespace SLT_Site.Controllers
{
    public class OverhoringController : Controller
    {
        public ActionResult OptieMenu(string id)
        {
            OptieMenuModel model = new OptieMenuModel
            {
                Opties = id
            };
            return View(model);
        }

        public ActionResult StartOverhoring(string id)
        {
            OverhoringLogic logic = new OverhoringLogic();
            StartOverhoringModel model = new StartOverhoringModel();
            if (id.Contains("?"))
            {               
                model.Overhoring = logic.Start(id.Split("?")[1], id.Split("?")[0]);
            }
            else
            {
                model.Overhoring = logic.Start(id, HttpContext.Session.GetString("Username"));
            }
            List<string[]> toJsonList = new List<string[]>();
            foreach (Woord w in model.Overhoring.WoordenLijst)
            {
                string[] s = new string[2];
                s[0] = w.Begrip;
                s[1] = w.Betekenis;
                toJsonList.Add(s);
            }
            model.Jsonstring = JsonConvert.SerializeObject(toJsonList);
            return View(model);

        }
    }
}