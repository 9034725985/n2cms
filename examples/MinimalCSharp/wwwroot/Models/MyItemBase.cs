using N2.Details;

namespace App.Models
{
	/// <summary>
	/// This is an abstract class that we can derive from on in all 
	/// situations when we want edit the item's title and name.
	/// </summary>
	[WithEditableTitle]
	public abstract class MyItemBase : N2.ContentItem
	{
	}
}