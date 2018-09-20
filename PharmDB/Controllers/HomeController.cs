using PharmDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmDB.Controllers
{
    public class HomeController : Controller
    {
        private PharmDBcontext db = new PharmDBcontext();

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
               return Redirect(Url.Action("Login", "Account"));
            }
            else
            {
               Category temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent1*DrewA*");
               ViewBag.Message1 = temp1.Description;
               temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent2*DrewA*");
               ViewBag.Message2 = temp1.Description;
               temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent3*DrewA*");
               ViewBag.Message3 = temp1.Description;
                return View();
            }
        }

        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
    }
}