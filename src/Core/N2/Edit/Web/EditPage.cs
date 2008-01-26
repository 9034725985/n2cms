using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Edit.Web
{
	/// <summary>
	/// Base class for edit mode pages. Provides functionality to parse 
	/// selected item and refresh navigation.
	/// </summary>
    public class EditPage : Page
    {
		protected override void OnInit(EventArgs e)
		{
			N2.Resources.Register.JQuery(this);
			base.OnInit(e);
		}

    	#region Refresh Methods
		private const string RefreshBothFormat = @"
if(window.top.n2)
{{
	window.top.n2.setupToolbar('{4}'); 
	window.top.n2.refresh('{1}', '{2}');
}}
else window.location = '{2}';";
		private const string RefreshNavigationFormat = @"
if(window.top.n2)
{{
	window.top.n2.setupToolbar('{4}'); 
	window.top.n2.refreshNavigation('{1}', '{2}');
}}";
		private const string RefreshPreviewFormat = @"
if(window.top.n2)
{{
	window.top.n2.setupToolbar('{4}'); 
	window.top.n2.refreshPreview('{1}', '{2}');
}}
else window.location = '{2}';";

		protected virtual void Refresh(ContentItem item, N2.Edit.ToolbarArea area)
		{
			string format;
			if (area == ToolbarArea.Both)
				format = RefreshBothFormat;
			else if (area == ToolbarArea.Preview)
				format = RefreshPreviewFormat;
			else
				format = RefreshNavigationFormat;

			string script = string.Format(format,
				Utility.ToAbsolute("~/Edit/Default.aspx"), // 0
				GetNavigationUrl(item), // 1
				GetPreviewUrl(item), // 2
				item.ID, // 3
				item.RewrittenUrl, // 4
				DataBinder.Eval(SelectedItem, "ID"), // 5
				DataBinder.Eval(SelectedItem, "RewrittenUrl") // 6
				);

			ClientScript.RegisterClientScriptBlock(
				typeof(EditPage),
				"AddRefreshEditScript",
				script, true);
		}

		protected string GetNavigationUrl(ContentItem selectedItem)
		{
			return Engine.EditManager.GetNavigationUrl(selectedItem);
		}

		protected string GetPreviewUrl(ContentItem selectedItem)
		{
			return Request["returnUrl"] ?? Engine.EditManager.GetPreviewUrl(selectedItem);
		}
		#endregion

		#region Setup Toolbar Methods
		protected virtual string SetupToolbarScriptFormat
		{
			get { return "if(window.top!=window && window.top.n2)window.top.n2.setupToolbar('{0}');"; }
		}

		protected virtual void RegisterSetupToolbarScript(ContentItem item)
		{
			string script = string.Format(SetupToolbarScriptFormat,
				item.RewrittenUrl,
				item.ID);

			ClientScript.RegisterClientScriptBlock(
				typeof(EditPage),
				"AddSetupToolbarScript",
				script, true);
		}

		#endregion

		#region Get Resource Methods
		protected string GetLocalResourceString(string resourceKey)
		{
			return (string)GetLocalResourceObject(resourceKey);
		}
		protected string GetGlobalResourceString(string className, string resourceKey)
		{
			return (string)GetGlobalResourceObject(className, resourceKey);
		} 

		#endregion

		#region Error Handling
		protected void SetErrorMessage(BaseValidator validator, N2.Integrity.NameOccupiedException ex)
		{
			Trace.Write(ex.ToString());

			string message = string.Format(GetLocalResourceString("NameOccupiedExceptionFormat"),
				ex.SourceItem.Name,
				ex.DestinationItem.Name);
			SetErrorMessage(validator, message);
		}

		protected void SetErrorMessage(BaseValidator validator, N2.Integrity.DestinationOnOrBelowItselfException ex)
		{
			Trace.Write(ex.ToString());

			string message = string.Format(GetLocalResourceString("DestinationOnOrBelowItselfExceptionFormat"),
				ex.SourceItem.Name,
				ex.DestinationItem.Name);
			SetErrorMessage(validator, message);
		}
		protected void SetErrorMessage(BaseValidator validator, N2.Definitions.NotAllowedParentException ex)
		{
			Trace.Write(ex.ToString());

			string message = string.Format(GetLocalResourceString("NotAllowedParentExceptionFormat"),
				ex.ItemDefinition.Title,
				Engine.Definitions.GetDefinition(ex.ParentType).Title);
			SetErrorMessage(validator, message);
		}

		protected void SetErrorMessage(BaseValidator validator, Exception exception)
		{
			Trace.Write(exception.ToString());

			SetErrorMessage(validator, exception.Message);
		}

		private void SetErrorMessage(BaseValidator validator, string message)
		{
			validator.IsValid = false;
			validator.ErrorMessage = message;
		}

		protected string GetBreadcrumbPath(ContentItem item)
		{
			string breadcrumb = "";
			for (; item != null; item = item.Parent)
				breadcrumb = item.Name + "/" + breadcrumb;
			return breadcrumb;
		}

		#endregion

		#region Properties

		public virtual Engine.IEngine Engine
		{
			get { return N2.Context.Instance; }
		}

		public override string ID
		{
			get { return base.ID ?? "P"; }
		}

		private int SelectedItemID
		{
			get { return (int)(ViewState["SelectedItemID"] ?? 0); }
			set { ViewState["SelectedItemID"] = value; }
		}

		private ContentItem selectedItem = null;

		/// <summary>Gets the currently selected item by the tree menu in edit mode.</summary>
		public virtual ContentItem SelectedItem
		{
			get
			{
				return selectedItem ?? 
					(selectedItem = GetFromViewState() 
						?? GetFromUrl() 
						?? N2.Context.UrlParser.StartPage);
			}
			set
			{
				selectedItem = value;

				if (value != null)
					SelectedItemID = value.ID;
				else
					SelectedItemID = 0;
			}
		}
		#endregion

		#region Helper Methods

		private ContentItem GetFromViewState()
		{
			if (SelectedItemID != 0)
				return N2.Context.Persister.Get(SelectedItemID);
			return null;
		}

		private ContentItem GetFromUrl()
		{
			string selected = GetSelectedUrl();
			if (!string.IsNullOrEmpty(selected))
				return N2.Context.UrlParser.Parse(selected);

			string itemId = Request["item"];
			if (!string.IsNullOrEmpty(itemId))
				return N2.Context.Persister.Get(int.Parse(itemId));

			return null;
		}

		protected string GetSelectedUrl()
		{
			return Request["selected"];
		}

		#endregion
    }
}
