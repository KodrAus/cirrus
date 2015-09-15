using System.Linq;
using System.Collections.Generic;
using Cirrus.Catalogue.Domain.Aggregates;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Data.DTOs;
using Xunit;

namespace Cirrus.Catalogue.Domain.Tests.ProductDTOs
{
	public class ProductDTOTests
	{
		[Fact]
		public void Can_Set_Variants()
		{
			AggregateFactory.Configure();
			
			var dto = new ProductDTO();

			dto.Variants = new List<Product>
			{
				new ProductDTO(),
				new ProductDTO()
			};

			var model = dto.AsAggregate<ProductVariantsAggregate>(new AggregateFactory());

			Assert.Equal(2, model.Variants.Count());
		}
	}
}