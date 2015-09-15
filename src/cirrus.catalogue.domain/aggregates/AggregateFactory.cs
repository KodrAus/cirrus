using System.Reflection;
using AutoMapper;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;

namespace Cirrus.Catalogue.Domain.Aggregates
{
	public class AggregateFactory : IAggregateFactory
	{
		public static void Configure()
		{
			//Shouldn't really call this outside of tests
			//Allowing AutoMapper to map non-public properties, this is essential if we're going to map the private product properties to our aggregates
			Mapper.Initialize(cfg =>
            {
                cfg.BindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            });
            
            Mapper.CreateMap<Product, ProductSummaryAggregate>();
            Mapper.CreateMap<Product, ProductDetailsAggregate>();
            Mapper.CreateMap<Product, ProductVariantsAggregate>();
		}

		public T ProductAsAggregate<T>(Product product) where T: Product
		{
			return Mapper.Map<T>(product);
		}
	}
}