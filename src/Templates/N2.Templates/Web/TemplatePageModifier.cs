using System.Collections.Generic;
using N2.Templates.Items;
using N2.Templates.Web.UI;

namespace N2.Templates.Web
{
	public class TemplatePageModifier : IPageModifierContainer
	{
		private readonly IList<IPageModifier> modifiers;

		public TemplatePageModifier(params IPageModifier[] modifiers)
		{
			this.modifiers = new List<IPageModifier>(modifiers);
		}

		public TemplatePageModifier()
			:this(new ThemeModifier(), new MasterPageModifier())
		{
		}

		public void Add(IPageModifier modifier)
		{
			modifiers.Add(modifier);
		}

		public void Remove(IPageModifier modifier)
		{
			modifiers.Remove(modifier);
		}

		public void Modify<T>(TemplatePage<T> page)
			where T : AbstractPage
		{
			foreach (IPageModifier adapter in modifiers)
			{
				adapter.Modify(page);
			}
		}
	}
}
