using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShibaStoreProject.Controllers
{
    public class HomeController : Controller
    {

        StoreDBEntities db = new StoreDBEntities();

        public ActionResult Index()
        {
            
            return View(db.Dogs.ToList());
        }
    }
}