using System;
using Cirrus.Test;
using Cirrus.Catalogue.Domain.Aggregates;

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

			runner.Run(() => Products.Assert.Has_Id());
			runner.Run(() => Products.Assert.Has_Title());
			runner.Run(() => Products.Assert.Has_Dynamic_Details());
			
			Console.WriteLine("Done");
		}
	}
}