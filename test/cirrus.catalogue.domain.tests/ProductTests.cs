using System.Linq;
using Cirrus.Catalogue.Domain.Aggregates;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;
using Xunit;

namespace Cirrus.Catalogue.Domain.Tests.Products
{
	public class ProductTests
	{
		[Fact]
		public void Has_Id()
		{
			string id = "346598635dfh";
			var product = new ProductBuilder().WithId(id).GetResult();

			Assert.Equal(product.Id, id);
		}

		[Fact]
		public void Has_Title()
		{
			string title = "My Product";
			var product = new ProductBuilder().WithTitle(title).GetResult();

			Assert.Equal(product.Title, title);
		}

		[Fact]
		public void Has_Description()
		{
			AggregateFactory.Configure();

			string desc = "My description";
			var product = new ProductBuilder().WithDescription(desc).AsAggregate<ProductDetailsAggregate>(new AggregateFactory());

			Assert.Equal(product.Description, desc);
		}

		[Fact]
		public void Has_Summary()
		{
			AggregateFactory.Configure();

			string summary = "My summary";
			var product = new ProductBuilder().WithSummary(summary).AsAggregate<ProductSummaryAggregate>(new AggregateFactory());

			Assert.Equal(product.Summary, summary);
		}

		[Fact]
		public void Has_Dynamic_Details()
		{
			string mySpecialField = "special things";
			var product = new Product();

			product.Details.MySpecialField = mySpecialField;

			Assert.Equal(product.Details.MySpecialField, mySpecialField);
		}

		[Fact]
		public void Has_Variants()
		{
			var product = new ProductBuilder()
				.WithVariants(
					new Product(),
					new Product()
				)
				.AsAggregate<ProductVariantsAggregate>(new AggregateFactory());

			Assert.Equal(product.Variants.Count(), 2);
		}
	}
}