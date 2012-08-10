﻿using System.Web.Mvc;
using MvcTest.Models;
using N2.Web;
using N2.Web.Mvc;
using MvcTest.Views.News;

namespace MvcTest.Controllers
{
	[Controls(typeof(NewsPage))]
	public class NewsController : ContentController<NewsPage>
	{
		public override ActionResult Index()
		{
			var vd = new NewsViewData 
			{ 
				News = CurrentItem, 
				Back = CurrentItem.Parent,
				Comments = CurrentItem.GetComments() 
			};
            return View("index", vd);
		}

		public ActionResult Comment()
		{
            return View("Comment", CurrentItem);
		}

		public ActionResult Submit(string title, string text)
		{
			CommentItem comment = Engine.Definitions.CreateInstance<CommentItem>(CurrentItem);
			comment.Title = Server.HtmlEncode(title);
			comment.Text = Server.HtmlEncode(text);
			Engine.Persister.Save(comment);

			return RedirectToAction("index");
		}
	}
}
