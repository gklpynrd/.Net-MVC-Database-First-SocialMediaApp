using SocialMediaApp.Helpers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaApp.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Post post)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            PostHelpers service = new PostHelpers();
            post.Userid = (Guid)Session["ID"];
            int i = service.UploadImageInDataBase(file, post);
            //if (i == 1)
            //{
            //    UserPost userPost = new UserPost { PostID = post.Postid, UserID = (Guid)Session["ID"] };
            //    DataClasses2DataContext dataClass = new DataClasses2DataContext();
            //    dataClass.UserPosts.InsertOnSubmit(userPost);
            //    dataClass.SubmitChanges();
            //}
            return RedirectToAction("Index", "Home");

        }
        [AllowAnonymous]
        public ActionResult RetrieveImage(int id)
        {
            try
            {
                byte[] img = ConnectionHelper.Context.Posts.FirstOrDefault(x => x.Postid == id).PostPicture.ToArray();
                return File(img, "image/jpg");
            }
            catch (NullReferenceException)
            {

                return null;
            }
        }

        [Route("/{Post}/{Delete}/{id}/{page}")]
        public ActionResult Delete(int id, string page)
        {
            ConnectionHelper.Context.Posts.DeleteOnSubmit(ConnectionHelper.Context.Posts.FirstOrDefault(x => x.Postid == id));
            ConnectionHelper.Context.SubmitChanges();
            var req = Request.Url;
            if (page == "Profile")
            {
                return Redirect("/Profile/Index/" + User.Identity.Name);
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}