using SocialMediaApp.App_Classes;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialMediaApp.Controllers
{
    public class SigninController : Controller
    {
        // GET: Singin
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Sign(User user, string PasswordCheck, string Terms)
        {
            string msg = "";
            if (user.Password != PasswordCheck)
            {
                msg = "Passwords must match!";
                TempData["Message"] = msg;
                return RedirectToAction("Index");
            }
            if (Terms != "agree")
            {
                msg = "Must agree the terms of service!";
                TempData["Message"] = msg;
                return RedirectToAction("Index");
            }
            MembershipCreateStatus status;
            Membership.CreateUser(user.UserName, user.Password, user.Email, user.SecretQuestion, user.SecretAnswer, true, out status);

            switch (status)
            {
                case MembershipCreateStatus.Success:
                    msg += "Success ";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    msg += "InvalidUserName ";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    msg += "InvalidPassword ";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    msg += "InvalidQuestion ";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    msg += "InvalidAnswer ";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    msg += "InvalidEmail ";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    msg += "DuplicateUserName ";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    msg += "DuplicateEmail ";
                    break;
                case MembershipCreateStatus.UserRejected:
                    msg += "UserRejected ";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    msg += "InvalidProviderUserKey ";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    msg += "DuplicateProviderUserKey ";
                    break;
                case MembershipCreateStatus.ProviderError:
                    msg += "ProviderError ";
                    break;
            }
            if (status == MembershipCreateStatus.Success)
            {
                return RedirectToAction("Index", "Login");
            }
            TempData["Message"] = msg;
            return RedirectToAction("Index");
        }
    }
}