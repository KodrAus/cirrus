using System;
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

			runner.Run(() => Products.Assert.Has_Id());
			runner.Run(() => Products.Assert.Has_Title());
			
			Console.WriteLine("Done");
		}
	}
}