using System.Linq;
using Cirrus.Catalogue.Domain.Aggregates.Products;

namespace Cirrus.Catalogue.Domain.Tests.Products
{
	static class Assert
	{
		public static bool Has_Id()
		{
			string id = "346598635dfh";
			var product = new ProductBuilder().WithId(id).GetResult();

			return product.Id == id;
		}

		public static bool Has_Title()
		{
			string title = "My Product";
			var product = new ProductBuilder().WithTitle(title).GetResult();

			return product.Title == title;
		}

		public static bool Has_Dynamic_Details()
		{
			string mySpecialField = "special things";
			var product = new Product();

			product.Details.MySpecialField = mySpecialField;

			return product.Details.MySpecialField == mySpecialField;
		}

		public static bool Has_Variants()
		{
			var product = new Product();

			return product.Variants.Count() == 1;
		}
	}
}