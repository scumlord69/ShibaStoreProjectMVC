using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShibaStoreProject.Controllers
{
    public class StoreController : Controller
    {

        StoreDBEntities db = new StoreDBEntities();

        public ActionResult Index()
        {

    
            return View(db.Dogs.ToList());
        }
        
        [HttpPost]
        public ActionResult Index(int dogID)
        {

            Dog dog = db.Dogs.First(i => i.DogID == dogID);
            int uID = Convert.ToInt32(System.Web.HttpContext.Current.Session["sessionID"]);
            User user = db.Users.First(i => i.UserID == uID);
            dog.Users.Add(user);
            db.SaveChanges();
            return View(db.Dogs.ToList());
        }
    }
}