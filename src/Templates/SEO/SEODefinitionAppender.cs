using Castle.Core;
using N2.Definitions;
using N2.Details;
using N2.Web.UI;

namespace N2.Templates.SEO
{
	public class SEODefinitionAppender : IStartable
	{
		private readonly IDefinitionManager definitions;
		private string titleTitle = "Page title";
		private string metaKeywordsTitle = "Meta keywords";
		private string metaDescriptionTitle = "Meta description";
		private string seoTabTitle = "SEO";

		public SEODefinitionAppender(IDefinitionManager definitions)
		{
			this.definitions = definitions;
		}

		public string TitleTitle
		{
			get { return titleTitle; }
			set { titleTitle = value; }
		}

		public string MetaKeywordsTitle
		{
			get { return metaKeywordsTitle; }
			set { metaKeywordsTitle = value; }
		}

		public string MetaDescriptionTitle
		{
			get { return metaDescriptionTitle; }
			set { metaDescriptionTitle = value; }
		}

		public string SeoTabTitle
		{
			get { return seoTabTitle; }
			set { seoTabTitle = value; }
		}

		#region IStartable Members

		public void Start()
		{
			foreach (ItemDefinition definition in definitions.GetDefinitions())
			{
				if(IsPage(definition))
				{
					TabPanelAttribute seoTab = new TabPanelAttribute("seo", SeoTabTitle, 30);
					definition.Add(seoTab);

					AddEditableText(definition, TitleTitle, TitleAndMetaTagApplyer.HeadTitle, 151, 50);
					AddEditableText(definition, MetaKeywordsTitle, TitleAndMetaTagApplyer.MetaKeywords, 152, 50);
					AddEditableText(definition, MetaDescriptionTitle, TitleAndMetaTagApplyer.MetaDescription, 153, 250);
				}
			}
		}

		private void AddEditableText(ItemDefinition definition, string title, string name, int sortOrder, int maxLength)
		{
			EditableTextBoxAttribute titleEditor = new EditableTextBoxAttribute(title, sortOrder, maxLength);
			titleEditor.Name = name;
			titleEditor.ContainerName = "seo";
			definition.Add(titleEditor);
		}

		private bool IsPage(ItemDefinition definition)
		{
			return typeof (Items.AbstractPage).IsAssignableFrom(definition.ItemType);
		}

		public void Stop()
		{
		}

		#endregion
	}
}
