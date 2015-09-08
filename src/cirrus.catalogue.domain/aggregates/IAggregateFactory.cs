using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;

//TODO: Move into a standard Cirrus.Core.Data package
namespace Cirrus.Catalogue.Domain.Aggregates
{
	public interface IAggregateFactory
	{
		T ProductAsAggregate<T>(Product product) where T: Product;
	}

	public static class ProductAggregateExtensions
	{
		public static T AsAggregate<T>(this Product product, IAggregateFactory factory)
			where T: Product
		{
			return factory.ProductAsAggregate<T>(product);
		}
	}
}