using System;
using System.Diagnostics;
using MbUnit.Framework;

namespace N2.Tests.Definitions
{
	[TestFixture]
	public class AttributeReference
	{
		public interface IVeichle
		{
			string GetDescription();
		}

		public class OffRoadAttribute : Attribute, IVeichle
		{
			public string GetDescription()
			{
				return "OffRoad";
			}
		}

		public class ReleaseYear : Attribute, IVeichle
		{
			private readonly int year;

			public ReleaseYear(int year)
			{
				this.year = year;
			}

			public string GetDescription()
			{
				return "Y" + year;
			}
		}

		[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
		public class FourWheelerAttribute : Attribute, IVeichle
		{
			private string description = "Y";

			public string Description
			{
				get { return description; }
				set { description = value; }
			}

			public string GetDescription()
			{
				return description;
			}
		}

		[OffRoad]
		public class Car
		{
		}

		[FourWheeler]
		public class Volvo : Car
		{
		}

		[OffRoad, FourWheeler(Description = "4wd optional")]
		public class VolvoV70 : Volvo
		{
		}

		[Test]
		public void AttributeExploration()
		{
			object[] a1 = typeof(Car).GetCustomAttributes(typeof(IVeichle), false);
			Assert.AreEqual(1, a1.Length);
			foreach (IVeichle i in a1)
				Debug.WriteLine("a1: " + i.GetDescription());
			Debug.WriteLine("");

			object[] a2 = typeof(Car).GetCustomAttributes(typeof(IVeichle), true);
			Assert.AreEqual(1, a2.Length);
			foreach (IVeichle i in a2)
				Debug.WriteLine("a2: " + i.GetDescription());
			Debug.WriteLine("");

			object[] b1 = typeof(Volvo).GetCustomAttributes(typeof(IVeichle), false);
			Assert.AreEqual(1, b1.Length);
			foreach (IVeichle i in b1)
				Debug.WriteLine("b1: " + i.GetDescription());
			Debug.WriteLine("");

			object[] b2 = typeof(Volvo).GetCustomAttributes(typeof(IVeichle), true);
			Assert.AreEqual(2, b2.Length);
			foreach (IVeichle i in b2)
				Debug.WriteLine("b2: " + i.GetDescription());
			Debug.WriteLine("");

			object[] c1 = typeof(VolvoV70).GetCustomAttributes(typeof(IVeichle), false);
			Assert.AreEqual(2, c1.Length);
			foreach (IVeichle i in c1)
				Debug.WriteLine("c1: " + i.GetDescription());
			Debug.WriteLine("");

			object[] c2 = typeof(VolvoV70).GetCustomAttributes(typeof(IVeichle), true);
			Assert.AreEqual(3, c2.Length);
			foreach (IVeichle i in c2)
				Debug.WriteLine("c2: " + i.GetDescription());
		}
	}
}
