using System.Web.UI;
using N2.Definitions;
using N2.Web.UI.WebControls;

[assembly : WebResource("N2.Resources.TabPanel.css", "text/css")]
[assembly : WebResource("N2.Resources.TabPanel.js", "text/javascript")]

namespace N2.Web.UI
{
	/// <summary>
	/// Defines a tab panel that can be used to contain editor controls.
	/// </summary>
	/// <example>
	///     [N2.Web.UI.TabPanel("default", "Default", 100)] // editables with the ContainerName="default" will be placed in a tab with the name "Default"
	///     public class MyPage : N2.ContentItem
	///     {
	///         [N2.Details.EditableFreeTextArea("Text", 110, ContainerName="default")]
	///         public virtual string Text
	///         {
	///             get { return (string)GetDetail("Text"); }
	///             set { SetDetail("Text", value); }
	///         }
	///     }
	/// </example>
	public class TabPanelAttribute : EditorContainerAttribute
	{
		private string tabText;
		private bool registerTabCss = false;

		public TabPanelAttribute(string name, string tabText, int sortOrder)
			: base(name, sortOrder)
		{
			TabText = tabText;
		}

		/// <summary>Gets or sets this panel's tab text.</summary>
		public string TabText
		{
			get { return tabText; }
			set { tabText = value; }
		}

		/// <summary>Gets or sets wether default styles should be registered.</summary>
		public bool RegisterTabCss
		{
			get { return registerTabCss; }
			set { registerTabCss = value; }
		}

		/// <summary>Adds the tab panel to a parent container and returns it.</summary>
		/// <param name="container">The parent container onto which to add the container defined by this interface.</param>
		/// <returns>The newly added tab panel.</returns>
		public override Control AddTo(Control container)
		{
			TabPanel p = new TabPanel();
			p.ID = Name;
			p.ToolTip = GetLocalizedText("TabText") ?? TabText;
			p.RegisterTabCss = RegisterTabCss;
			container.Controls.Add(p);
			return p;
		}
	}
}