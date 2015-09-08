using System;
using System.Collections.Generic;
using Cirrus.Catalogue.Domain.Aggregates;
using Cirrus.Test;

namespace Cirrus.Catalogue.Domain.Tests
{
	public class Program
	{
		static void Main(string[] args)
		{
			//Run tests
			AggregateFactory.Configure();
			var runner = new TestRunner();

			Console.WriteLine("Testing...");

			runner.Run(() => ProductDTOs.Assert.Can_Set_Variants());
			runner.Run(() => ProductRepositories.Assert.Can_Index_Products());
			runner.Run(() => ProductRepositories.Assert.Can_Partially_Update_Product_Variants());
			runner.Run(() => ProductRepositories.Assert.Can_Get_All_Variants());
			runner.Run(() => ProductRepositories.Assert.Can_Index_Recursive_Variants());

			Console.WriteLine("Done");
		}
	}
}