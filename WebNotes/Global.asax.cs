using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebNotes.EF;

namespace WebNotes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            //SQLiteConnection.CreateFile(Server.MapPath("~/App_Data/WebNotes.sqlite"));
            
        }
    }
}
