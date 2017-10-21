using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.IO;

using rtsweb.Models;

namespace rtsweb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            HomeRepository.Instance();
            NoticeRepository.Instance();
            GlobRepository.Instance();
            UserRespository.Instance();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
#if !DEBUG
            if (Context.Request.FilePath == "/")
            {
                Context.RewritePath("index.html");
            }
            if (!Directory.Exists("Upload"))
            {
                Directory.CreateDirectory("Upload");
                Directory.CreateDirectory("Upload/Temploate");
                Directory.CreateDirectory("Upload/ImageNews");
            }
#endif
        }
    }
}
