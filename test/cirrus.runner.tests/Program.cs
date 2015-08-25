using System;
using Cirrus.Test;

namespace Cirrus.Test.Runner.Tests
{
	class Program
	{
		//Runner
		public static void Main(string[] args)
		{
			var runner = new TestRunner();

			Console.WriteLine("Testing...");

			//Run a test
			//TODO: Use reflection to get all tests automagically
			runner.Run(() => Assert.TestRunner_Executes_Methods());
			runner.Run(() => Assert.TestRunner_Passes_Through_Exceptions());

			Console.WriteLine("Done");
		}
	}

	//Tests
	static class Assert
	{
		public static bool TestRunner_Executes_Methods()
		{
			return true;
		}

		public static bool TestRunner_Passes_Through_Exceptions()
		{
			throw new Exception("ERRORS and stuff");
		}
	}
}