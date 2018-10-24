using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShibaStoreProject.Controllers
{
    public class LoginController : Controller
    {
        StoreDBEntities db = new StoreDBEntities();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {

            
            if (db.Users.Any(o => o.Username == username && o.Password == password))
            {

                User user = db.Users.First(i => i.Username == username && i.Password == password);
                
                System.Web.HttpContext.Current.Session.Add("sessionID", user.UserID.ToString());

                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(username, false);
                return RedirectToAction("SuccessfulLogin");
            }
            ViewBag.Error = "You have entered incorrect information. :(";
            return View();
        }

        public ActionResult SuccessfulLogin()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username, string password, string passwordRepeat)
        {
            if (db.Users.Any(o => o.Username == username)){
                ViewBag.Error = "Username already exists. :(";
                return View();
            }else if(password != passwordRepeat)
            {
                ViewBag.Error = "Password does not match. :(";
                return View();
            }
            else
            {
                User newUser = new User();
                newUser.Username = username;
                newUser.Password = password;
                db.Users.Add(newUser);
                db.SaveChanges();
                System.Web.HttpContext.Current.Session.Add("registerVar", "registered");
                return RedirectToAction("SuccessfulRegister");

            }
        }

        public ActionResult SuccessfulRegister()
        {
            if(System.Web.HttpContext.Current.Session["registerVar"] != null)
            {
                return View();

            }
            return RedirectToAction("Index", "Home");
        }
    }
    
}