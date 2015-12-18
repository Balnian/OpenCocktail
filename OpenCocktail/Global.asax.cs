using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OpenCocktail
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            String DB_SourceCocktails_Path = Server.MapPath(@"~\App_Data\SourceCocktails.mdf");
            Session["DB"] = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='" + DB_SourceCocktails_Path + "'; Integrated Security=true; Max Pool Size=1024; Pooling=true;";
        }
    }
}
