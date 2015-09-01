using System;
using System.Collections.Generic;
using Cirrus.Catalogue.Domain.Aggregates.Products;
using Cirrus.Catalogue.Domain.Aggregates.Products.ValueObjects;
using Cirrus.Catalogue.Data.DTOs;
using Cirrus.Catalogue.Data.Repositories;
using Cirrus.Test;

namespace Cirrus.Catalogue.Domain.Tests
{
	public class Program
	{
		static void Main(string[] args)
		{
			//Run tests
			var runner = new TestRunner();

			Console.WriteLine("Testing...");

			runner.Run(() => ProductDTOs.Assert.Can_Set_Variants());
			runner.Run(() => ProductVariantDTOs.Assert.Can_Set_Id());
			runner.Run(() => ProductVariantDTOs.Assert.Can_Set_Title());
			runner.Run(() => ProductRepositories.Assert.Can_Index_Products());
			runner.Run(() => ProductRepositories.Assert.Can_Partially_Update_Product_Variants());
			runner.Run(() => ProductRepositories.Assert.Can_Get_All_Variants());

			Console.WriteLine("Done");
		}
	}
}