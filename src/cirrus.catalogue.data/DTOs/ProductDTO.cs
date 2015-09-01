using System.Collections.Generic;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Domain.Aggregates.Products.ValueObjects;

namespace Cirrus.Catalogue.Data.DTOs
{
	public class ProductDTO : Product
	{	
		public new string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		public new IEnumerable<Variant> Variants
		{
			get
			{
				return _variants;
			}
			set
			{
				_variants = value;
			}
		}
	}
}