using Cirrus.Catalogue.Domain.Aggregates;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;
using Xunit;

namespace Cirrus.Catalogue.Domain.Tests
{
	public class CategoryTests
	{
		[Fact]
		public void Has_Title()
		{
			string title = "Some Category";

			var category = new Category(title);

			Assert.Equal(category.Title, title);
		}
	}
}