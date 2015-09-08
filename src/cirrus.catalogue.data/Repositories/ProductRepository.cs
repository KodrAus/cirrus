using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Cirrus.Catalogue.Domain.Aggregates;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;
using Cirrus.Catalogue.Data.DTOs;
using Nest;
using AutoMapper;

namespace Cirrus.Catalogue.Data.Repositories
{
	public class ProductRepository : IProductRepository
	{
		public ProductRepository(IAggregateFactory aggregateFactory)
		{
			_client = new ElasticClient();
			_factory = aggregateFactory;

			Mapper.CreateMap<Product, ProductDTO>();
		}

		private ElasticClient _client;
		private readonly IAggregateFactory _factory;

		public async Task IndexAsync(Product product)
		{
			var dto = Mapper.Map<ProductDTO>(product);

			//Update the variants partially, using the script
			//This method isn't going to support n-recursive product variants
			foreach (var variant in dto.Variants)
			{
				var variantResponse = await _client.UpdateAsync<ProductDTO>(body => body
					.Id(dto.Id)

					.Params(pms => pms.Add("new_variant", variant))
					.Script(Transactional.Scripts.update_variants)
					.Lang("groovy")

					.Index("products")
					.Upsert(dto)
					.Type<ProductDTO>()
				);

				if (!variantResponse.ConnectionStatus.Success)
				{
					Console.WriteLine(Encoding.Default.GetString(variantResponse.ConnectionStatus.Request));
					throw variantResponse.ConnectionStatus.OriginalException;
				}
			}

			//TODO: Update the rest of the document
			//await _client.IndexAsync(dto, idx => idx.Index("products"));
		}

		public async Task<ProductSummaryAggregate> GetSummaryAsync(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<ProductVariantsAggregate> GetVariantsAsync(string id)
		{
			//TODO: Only return appropriate source fields, not everything
			var result = await _client.GetAsync<ProductDTO>(id, "products");

			var product = result.Source;

			return product.AsAggregate<ProductVariantsAggregate>(_factory);
		}

		public async Task<ProductVariantsAggregate> GetNestedVariantsAsync(params string[] idPath)
		{
			//Each id in the path constitutes a variant
			//So [id1, id2, id3] -> _source.variants[id2].variants[id3]
			
			throw new NotImplementedException();
		}
	}
}