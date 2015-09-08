using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;

namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	public class ProductBuilder : Product
	{
		public ProductBuilder WithId(string id)
		{
			_id = id;

			return this;
		}

		public ProductBuilder WithTitle(string title)
		{
			_title = title;

			return this;
		}

		public ProductBuilder WithVariants(params Product[] variants)
		{
			_variants = variants;

			return this;
		}

		public Product GetResult()
		{
			return this;
		}
	}
}