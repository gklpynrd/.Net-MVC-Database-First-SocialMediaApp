using SocialMediaApp.Helpers;
using SocialMediaApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialMediaApp.Controllers
{
    public class LoginController : Controller
    {        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(App_Classes.User user, string remember)
        {

            bool rem = remember == "on" ? true : false;
            if (Membership.ValidateUser(user.UserName, user.Password))
            {
                aspnet_User current = ConnectionHelper.Context.aspnet_Users.FirstOrDefault(x => x.UserName == user.UserName);
                Session["ID"] = current.UserId;
                FormsAuthentication.RedirectFromLoginPage(user.UserName, rem);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Username Or Password is Wrong";
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["ID"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Recover()
        {
            return View();
        }
    }
}