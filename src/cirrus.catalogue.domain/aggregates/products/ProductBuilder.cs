namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	public class ProductBuilder : Product
	{
		public ProductBuilder WithId(string id)
		{
			Id = id;

			return this;
		}

		public ProductBuilder WithTitle(string title)
		{
			Title = title;

			return this;
		}

		public Product GetResult()
		{
			return this;
		}
	}
}