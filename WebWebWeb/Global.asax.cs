using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebWebWeb.BL;

namespace WebWebWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Default", action = "Info", id = "" }  // Parameter defaults
            );
        }
        public void Session_OnEnd()
        {
            try
            {
                string nick_name = (string)Session["nick_name"];
                MemberProfilBL.IS_ONLINE(nick_name, false);
            }
            catch
            {
                
            }
        }       
    }
}
