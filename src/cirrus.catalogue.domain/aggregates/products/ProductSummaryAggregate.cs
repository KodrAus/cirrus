using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;

namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	public class ProductSummaryAggregate : Product
	{
		public new string Summary
		{
			get
			{
				return _summary;
			}
		}
	}
}