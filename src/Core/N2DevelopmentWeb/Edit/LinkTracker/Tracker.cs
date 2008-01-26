using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using N2.Details;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;
using Castle.Core;

namespace N2.Edit.LinkTracker
{
	/// <summary>This class examines saved items and keeps tracks of links in html.</summary>
	public class Tracker : IStartable
	{
		public const string LinkDetailName = "TrackedLinks";
		Persistence.IPersister persister;
		Persistence.Finder.IItemFinder find;
		N2.Web.IUrlParser urlParser;

		public Tracker(Persistence.IPersister persister, Persistence.Finder.IItemFinder find, N2.Web.IUrlParser urlParser)
		{
			this.persister = persister;
			this.find = find;
			this.urlParser = urlParser;
		}

		void persister_ItemSaving(object sender, N2.Persistence.CancellableItemEventArgs e)
		{
			OnTrackingLinks(e.AffectedItem);
		}

		#region Methods
		/// <summary>Is invoked when an item is beeing saved.</summary>
		/// <param name="item">The item that is beeing saved.</param>
		protected virtual void OnTrackingLinks(ContentItem item)
		{
			IList<ContentItem> referencedItems = FindLinkedItems(item);
			DetailCollection links = item.GetDetailCollection(LinkDetailName, false);
			if (referencedItems.Count > 0)
			{
				if (links == null)
					links = item.GetDetailCollection(LinkDetailName, true);
				links.Replace(referencedItems);
			}
			else if (links != null && links.Count > 0)
			{
				links.Clear();
			}
		}

		/// <summary>Finds items linked by the supplied item. This method only finds links in html text to items within the site.</summary>
		/// <param name="item">The item to examine for links.</param>
		/// <returns>A list of items referenced in html text by the supplied item.</returns>
		public virtual IList<ContentItem> FindLinkedItems(ContentItem item)
		{
			N2.Collections.ItemList items = new N2.Collections.ItemList();
			foreach (ContentDetail detail in item.Details.Values)
			{
				if (detail is N2.Details.StringDetail)
				{
					foreach (string link in FindLinks(((StringDetail)detail).StringValue))
					{
						try
						{
							ContentItem referencedItem = urlParser.Parse(link);
							if (referencedItem != null && !items.Contains(referencedItem))
							{
								items.Add(referencedItem);
							}
						}
						catch (HttpException ex)
						{
							Trace.TraceWarning(ex.Message);
						}
					}
				}
			}
			return items;
		}

		/// <summary>Finds links in a html string using regular expressions.</summary>
		/// <param name="html">The html to search for links.</param>
		/// <returns>A list of link (a) href attributes in the supplied html string.</returns>
		public virtual IList<string> FindLinks(string html)
		{
			List<string> links = new List<string>();
			MatchCollection matches = Regex.Matches(html, "<a.*?href=[\"']*(?<link>[^\"'>]*).*?</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			foreach (Match m in matches)
			{
				if (m.Groups["link"].Success)
					links.Add(m.Groups["link"].Value);
			}
			return links;
		}

		/// <summary>Finds other whose links are beeing tracked by the link tracker. This doesn't include other links that might exist.</summary>
		/// <param name="item">The item is beeing referenced by the items we'd like to find.</param>
		/// <returns>A list of items linking to the supplied item.</returns>
		public virtual IList<ContentItem> FindReferrers(ContentItem item)
		{
			return find.Where.Detail(LinkDetailName).Eq(item)
				.Filters(new Collections.DuplicateFilter())
				.Select();
		} 
		#endregion

		#region IStartable Members

		public void Start()
		{
			persister.ItemSaving += new EventHandler<N2.Persistence.CancellableItemEventArgs>(persister_ItemSaving);
		}

		public void Stop()
		{
			persister.ItemSaving -= new EventHandler<N2.Persistence.CancellableItemEventArgs>(persister_ItemSaving);
		}

		#endregion
	}
}
