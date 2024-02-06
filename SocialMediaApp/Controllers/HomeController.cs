using SocialMediaApp.Helpers;
using SocialMediaApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace SocialMediaApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Post> List;

            //if (Session["ID"] != null)
            //{
            //    List = ConnectionHelper.Context.Posts.Where(x => x.Userid == (Guid)Session["ID"]).Take(25).OrderByDescending(x => x.Postid).ToList();
            //    ViewBag.User = ConnectionHelper.Context.aspnet_Users.FirstOrDefault(x => x.UserId == (Guid)Session["ID"]);
            //}
            //else
            //{
            List = ConnectionHelper.Context.Posts.Take(25).OrderByDescending(x => x.Postid).ToList();
            //}
            return View(List);
        }
    }
}