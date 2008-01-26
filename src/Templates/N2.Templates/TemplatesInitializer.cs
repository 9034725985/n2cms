using N2.Engine;
using N2.Plugin;
using N2.Templates.Web;

namespace N2.Templates
{
	[AutoInitialize]
	public class TemplatesInitializer : IPluginInitializer
	{
		#region IPluginInitializer Members

		public void Initialize(IEngine engine)
		{
			engine.AddComponent("n2.templates.pagemodifier", typeof(IPageModifierContainer), typeof (TemplatePageModifier));
		}

		#endregion
	}
}
