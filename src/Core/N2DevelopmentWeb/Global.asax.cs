using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using log4net;
using System.Diagnostics;

namespace N2.TemplateWeb
{
    public class Global : System.Web.HttpApplication
    {
		public static log4net.ILog log;

        protected void Application_Start(object sender, EventArgs e)
        {
			AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_DomainUnload);
			log4net.Config.XmlConfigurator.Configure();
			//log = LogManager.GetLogger("default");
			//log.Error("Application_Start");
			Debug.WriteLine("Application_Start");
        }

        void UrlParser_PageNotFound(object sender, N2.Web.PageNotFoundEventArgs e)
        {
            e.AffectedItem = N2.Context.Persister.Get(3);
        }

		void CurrentDomain_DomainUnload(object sender, EventArgs e)
		{
			Debug.WriteLine("CurrentDomain_DomainUnload");
		}

		public override void Init()
		{
			base.Init();
			Debug.WriteLine("Init");
			//log.Error("Init");
            N2.Context.UrlParser.PageNotFound += new EventHandler<N2.Web.PageNotFoundEventArgs>(UrlParser_PageNotFound);
        }

        protected void Application_End(object sender, EventArgs e)
        {
			Debug.WriteLine("Application_End");
			//log.Error("Application_End");
        }

		public override void Dispose()
		{
			Debug.WriteLine("Global.Dispose");
			base.Dispose();
		}

		~Global()
		{
			//Debug.WriteLine("~Global");
		}
    }
}
