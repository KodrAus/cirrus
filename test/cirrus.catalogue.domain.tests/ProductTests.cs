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
	}
}