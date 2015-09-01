namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	public interface IProduct
	{
		string Id { get; }
		string Title { get; }

		dynamic Details { get; set; }
	}
}