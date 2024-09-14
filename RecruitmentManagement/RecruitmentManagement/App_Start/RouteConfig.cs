using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace RecruitmentManagement
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "EntryPage", id = UrlParameter.Optional });

            ////defaults: new { controller = "Job", action = "Index", id = UrlParameter.Optional });
            //defaults: new { controller = "Candidate", action = "Index", id = UrlParameter.Optional });
            //defaults: new { controller = "Interview", action = "Index", id = UrlParameter.Optional });



        }
    }
}
