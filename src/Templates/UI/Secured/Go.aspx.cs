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

namespace N2.Templates.UI.Secured
{
	public partial class Go : N2.Edit.Web.EditPage
	{
		protected override void OnInit(EventArgs e)
		{
			Response.Redirect(SelectedItem.Url);
		}
	}
}
