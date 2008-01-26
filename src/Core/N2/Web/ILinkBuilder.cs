using System.Web.UI;

namespace N2.Web
{
	/// <summary>
	/// Builds and modifies a link to somewhere.
	/// </summary>
	public interface ILinkBuilder : ILink
	{
		/// <summary>Sets the link href.</summary>
		/// <param name="href">An url.</param>
		/// <returns>The same object for chaining.</returns>
		ILinkBuilder Href(string href);
		/// <summary>Sets the link text.</summary>
		/// <param name="text">A string or html.</param>
		/// <returns>The same object for chaining.</returns>
		ILinkBuilder Text(string text);
		/// <summary>Sets the link target frame.</summary>
		/// <param name="target">A window or frame name.</param>
		/// <returns>The same object for chaining.</returns>
		ILinkBuilder Target(string target);
		/// <summary>Sets the link title/tooltip.</summary>
		/// <param name="title">A string.</param>
		/// <returns>The same object for chaining.</returns>
		ILinkBuilder Title(string title);

		/// <summary>Sets the link class.</summary>
		/// <param name="className">One or more CSS classes.</param>
		/// <returns>The same object for chaining.</returns>
		ILinkBuilder Class(string className);
		/// <summary>Sets the href's query string.</summary>
		/// <param name="query">A query string with one or more query parameters.</param>
		/// <returns>The same object for chaining.</returns>
		ILinkBuilder Query(string query);
		/// <summary>Adds a query key value pair to any existing query.</summary>
		/// <param name="key">The query key.</param>
		/// <param name="value">The query value.</param>
		/// <returns>The same object for chaining.</returns>
		ILinkBuilder AddQuery(string key, string value);

		/// <summary>Gets the link's string representation.</summary>
		/// <returns>A string anchor.</returns>
		string ToString();
		/// <summary>Creates an anchor control representing the link.</summary>
		/// <returns>An anchor control.</returns>
		Control ToControl();
	}
}
