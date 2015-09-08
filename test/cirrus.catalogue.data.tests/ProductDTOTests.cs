using System.Linq;
using System.Collections.Generic;
using Cirrus.Catalogue.Domain.Aggregates;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Data.DTOs;

namespace Cirrus.Catalogue.Domain.Tests.ProductDTOs
{
	static class Assert
	{
		public static bool Can_Set_Variants()
		{
			var dto = new ProductDTO();

			dto.Variants = new List<Product>
			{
				new ProductDTO(),
				new ProductDTO()
			};

			var model = dto.AsAggregate<ProductVariantsAggregate>(new AggregateFactory());

			return model.Variants.Count() == 2;
		}
	}
}