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
			set
			{
				_summary = value;
			}
		}
		
		public new string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		public new IEnumerable<Category> Categories
		{
			get
			{
				return _categories;
			}
			set
			{
				_categories = value;
			}
		}
	}
}