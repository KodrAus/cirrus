using System.Linq;
using System.Collections.Generic;
using Cirrus.Catalogue.Domain.Aggregates.Products.ValueObjects;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Data.DTOs;

namespace Cirrus.Catalogue.Domain.Tests.ProductDTOs
{
	static class Assert
	{
		public static bool Can_Set_Variants()
		{
			var dto = new ProductDTO();

			dto.Variants = new List<Variant>
			{
				new VariantDTO(),
				new VariantDTO()
			};

			Product model = dto;

			return model.Variants.Count() == 2;
		}
	}
}