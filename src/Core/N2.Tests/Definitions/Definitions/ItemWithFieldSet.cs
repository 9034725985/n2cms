using System;
using System.Collections.Generic;
using System.Text;

namespace N2.Tests.Definitions.Definitions
{
	[N2.Web.UI.FieldSet("default", "Default Fieldset", 0)]
	public class ItemWithFieldSet : N2.ContentItem
	{
		[N2.Details.Editable("My Property", typeof(System.Web.UI.WebControls.TextBox), "Text", 100, ContainerName = "default")]
		public virtual string MyProperty
		{
			get { return (string)(GetDetail("MyProperty") ?? ""); }
			set { SetDetail<string>("MyProperty", value); }
		}
	}
}
