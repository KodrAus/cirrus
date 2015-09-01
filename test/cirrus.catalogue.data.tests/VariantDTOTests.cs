using System.Linq;
using System.Dynamic;
using Cirrus.Catalogue.Data.DTOs;
using Cirrus.Catalogue.Domain.Aggregates.Products.ValueObjects;

namespace Cirrus.Catalogue.Domain.Tests.ProductVariantDTOs
{
	static class Assert
	{
		public static bool Can_Set_Id()
		{
			var id = "my id";

			var dto = new VariantDTO();

			dto.Id = id;

			Variant model = dto;

			return model.Id == dto.Id;
		}

		public static bool Can_Set_Title()
		{
			var title = "my title";

			var dto = new VariantDTO();

			dto.Title = title;

			Variant model = dto;

			return model.Title == dto.Title;
		}
	}
}