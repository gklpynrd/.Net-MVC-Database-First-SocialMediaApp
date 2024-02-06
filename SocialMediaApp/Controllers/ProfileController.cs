using SocialMediaApp.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SocialMediaApp.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(string id)
        {
            var user = ConnectionHelper.Context.aspnet_Users.FirstOrDefault(x => x.UserName == id);
            ViewBag.Posts = ConnectionHelper.Context.Posts.Where(x => x.Userid == user.UserId).ToList();
            return View(user);
        }

        public ActionResult RetrieveImage(Guid id)
        {
            byte[] img = ConnectionHelper.Context.aspnet_Users.FirstOrDefault(x => x.UserId == id).ProfilePicture.ToArray();
            if (img != null)
            {
                return File(img, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public ActionResult Search()
        {
            if (TempData["Profiles"] != null)
            {
                ViewBag.Profiles = TempData["Profiles"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Search(string UserName)
        {
            TempData["Profiles"] = ConnectionHelper.Context.aspnet_Users.Where(x => x.UserName.Contains(UserName)).Take(10).ToList();
            return RedirectToAction("Search");
        }
    }
}