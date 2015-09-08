using System.Collections.Generic;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;

namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	public class ProductVariantsAggregate : Product
	{
		public new IEnumerable<Product> Variants
		{
			get
			{
				return _variants;
			}
		}
	}
}