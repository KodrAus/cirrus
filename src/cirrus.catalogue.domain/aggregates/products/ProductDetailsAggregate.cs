using System.Collections.Generic;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;

namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	public class ProductDetailsAggregate : ProductVariantsAggregate
	{
		public new string Summary
		{
			get
			{
				return _summary;
			}
		}
		
		public new string Description
		{
			get
			{
				return _description;
			}
		}

		public new IEnumerable<Category> Categories
		{
			get
			{
				return _categories;
			}
		}
	}
}