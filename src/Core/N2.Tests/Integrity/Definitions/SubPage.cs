using System;
using System.Collections.Generic;
using System.Text;

namespace N2.Tests.Integrity.Definitions
{
	[N2.Integrity.RestrictParents(typeof(Page))] // AllowedItemBelowRoot as parent allowed
	public class SubPage : N2.ContentItem
	{
	}
}
