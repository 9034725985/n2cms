using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace N2.TemplateWeb
{
	public partial class AutomaticallyUpdatedTest : N2.Web.UI.Page<Domain.MyPageData>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			CurrentItem.Updated = DateTime.Now;
		}

	}
}
