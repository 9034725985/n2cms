﻿using N2;
using System.Collections.Generic;
using N2.Collections;

namespace MvcTest.Models
{
	[Definition("News Collection")]
	public class NewsContainer : AbstractPage
	{
		public virtual IEnumerable<NewsPage> GetNews()
		{
			return GetChildren(new AccessFilter(), new TypeFilter(typeof (NewsPage))).Cast<NewsPage>();
		}
	}
}
