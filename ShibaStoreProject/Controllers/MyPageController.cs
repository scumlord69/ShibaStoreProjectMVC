using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShibaStoreProject.Controllers
{
    public class MyPageController : Controller
    {

        StoreDBEntities db = new StoreDBEntities();

        // GET: MyPage

        [Authorize]
        public ActionResult Index()
        {

            List<Dog> doglist = new List<Dog>();
            foreach (var dogs in db.Dogs)
            {
                var uID = Convert.ToInt32(System.Web.HttpContext.Current.Session["sessionID"]);
                User users = db.Users.First(i => i.UserID == uID);

                if (db.Dogs.FirstOrDefault(i => i.DogID == dogs.DogID && i.Users.Any(t => t.UserID == uID)) != null)
                {
                    doglist.Add(dogs);
                }
            }
            return View(doglist);
        }
       [HttpPost]
       public ActionResult Index(int dogID)
        {
            var uID = Convert.ToInt32(System.Web.HttpContext.Current.Session["sessionID"]);
            User users = db.Users.First(i => i.UserID == uID);
            Dog dog = db.Dogs.First(i => i.DogID == dogID);
            dog.Users.Remove(users);
            db.SaveChanges();

            List<Dog> doglist = new List<Dog>();
            foreach (var dogs in db.Dogs)
            {
                

                if (db.Dogs.FirstOrDefault(i => i.DogID == dogs.DogID && i.Users.Any(t => t.UserID == uID)) != null)
                {
                    doglist.Add(dogs);
                }
            }
            return View(doglist);
        }
    }
}