using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Cirrus.Catalogue.Domain.Aggregates;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Domain.Aggregates.Products.Entities;
using Cirrus.Catalogue.Data.DTOs;
using Cirrus.Catalogue.Data.Repositories;

namespace Cirrus.Catalogue.Domain.Tests.ProductRepositories
{
	static class Assert
	{
		public static bool Can_Index_Products()
		{
			var repository = new ProductRepository(new AggregateFactory());

			var product = new ProductDTO();
			product.Id = Guid.NewGuid().ToString();

			var variant = new ProductDTO();

			variant.Id = "my id";
			variant.Title = "my title";

			variant.Details.CustomField1 = "a string value";
			variant.Details.CustomField2 = 42;

			product.Variants = new List<Product>
			{
				variant
			};

			repository.IndexAsync(product).Wait();

			var result = repository.GetSummaryAsync(product.Id).Result;

			return product.Title == result.Title;
		}

		public static bool Can_Partially_Update_Product_Variants()
		{
			var repository = new ProductRepository(new AggregateFactory());

			//Create a product
			var product = new ProductDTO();
			product.Id = Guid.NewGuid().ToString();

			//Create a variant
			var variant1 = new ProductDTO();

			variant1.Id = "1";
			variant1.Title = "my title 1";

			variant1.Details.CustomField1 = "a string value";
			variant1.Details.CustomField2 = 42;

			//Create another variant
			var variant2 = new ProductDTO();

			variant2.Id = "2";
			variant2.Title = "my title 2";

			product.Variants = new List<Product>
			{
				variant1,
				variant2
			};

			Console.WriteLine("Indexing as " + product.Id);

			try
			{
				repository.IndexAsync(product).Wait();
			}
			catch (AggregateException ae)
		    {
		    	Console.WriteLine(ae.InnerExceptions[0].Message);
		        throw ae.Flatten();
		    }


			Task.Delay(500).Wait();

			//Create a 'copy' of the product, and reindex with just the second variant
			//Should update the second variant, but leave the first untouched

			var newProduct = new ProductDTO();
			newProduct.Id = product.Id;

			variant2.Title = "my new title 2";

			newProduct.Variants = new List<Product>
			{
				variant2
			};

			Console.WriteLine("Reindexing as " + product.Id);

			//TODO: Make this method work as expected
			repository.IndexAsync(newProduct).Wait();

			throw new NotImplementedException();
		}

		public static bool Can_Get_All_Variants()
		{
			var repository = new ProductRepository(new AggregateFactory());

			var product = new ProductDTO();
			product.Id = Guid.NewGuid().ToString();

			var variant = new ProductDTO();

			variant.Id = "my id";
			variant.Title = "my title";

			variant.Details.CustomField1 = "a string value";
			variant.Details.CustomField2 = 42;

			product.Variants = new List<Product>
			{
				variant
			};

			repository.IndexAsync(product).Wait();

			Task.Delay(500).Wait();

			var result = repository.GetVariantsAsync(product.Id).Result;

			return result.Variants.Count() == 1;
		}

		public static bool Can_Index_Recursive_Variants()
		{
			var repository = new ProductRepository(new AggregateFactory());

			//Create a product
			var product = new ProductDTO();
			product.Id = Guid.NewGuid().ToString();

			//Create a variant
			var variant = new ProductDTO();

			variant.Id = "my id";
			variant.Title = "my title";

			variant.Details.CustomField1 = "a string value";
			variant.Details.CustomField2 = 42;

			//Create a nested variant
			var nestedVariant = new ProductDTO();

			nestedVariant.Id = "my nested id";
			nestedVariant.Title = "my nested title";

			nestedVariant.Details.CustomField3 = DateTime.Now;

			variant.Variants = new List<Product>
			{
				nestedVariant
			};

			product.Variants = new List<Product>
			{
				variant
			};

			Console.WriteLine("Indexing as " + product.Id);

			repository.IndexAsync(product).Wait();

			Task.Delay(500).Wait();

			var result = repository.GetVariantsAsync(product.Id).Result;

			return result.Variants.Count() == 1;
		}
	}
}