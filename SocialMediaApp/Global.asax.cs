using SocialMediaApp.Helpers;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SocialMediaApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            if (User.Identity.IsAuthenticated)
            {
                Session["ID"] = ConnectionHelper.Context.aspnet_Users.FirstOrDefault(x => x.UserName == User.Identity.Name).UserId;
            }
        }

        protected void Session_End()
        {
            Session["ID"] = null;
        }
    }
}
