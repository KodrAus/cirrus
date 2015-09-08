using System.Collections.Generic;
using System.Threading.Tasks;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;

namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	public interface IProductRepository
	{
		Task IndexAsync(Product product);
		Task<ProductSummaryAggregate> GetSummaryAsync(string id);
		Task<ProductVariantsAggregate> GetVariantsAsync(string id);
	}
}