using System.Linq;
using System.Dynamic;
using Cirrus.Catalogue.Domain.Aggregates.Products.ValueObjects;

namespace Cirrus.Catalogue.Domain.Tests.ProductVariants
{
	static class Assert
	{
		public static bool Has_Dynamic_Details()
		{
			var variant = new Variant();

			variant.Details.Something = "my special value";

			return variant.Details.Something == "my special value";
		}
	}
}