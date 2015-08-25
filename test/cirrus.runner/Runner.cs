using System;
using System.Linq.Expressions;

namespace Cirrus.Test
{
	public class TestRunner
	{
		//TODO: Add async support
		public void Run(Expression<Func<bool>> test)
		{
			var testName = ((MethodCallExpression)test.Body).Method.Name;

			var consolePrefix = string.Format("TestRunner: '{0}'", testName);
			Console.WriteLine(consolePrefix + " Running");

			var testAction = test.Compile();

			try
			{
				var testResult = testAction();

				Console.WriteLine(consolePrefix + " " + testResult);
			}
			catch (Exception e)
			{
				Console.WriteLine(consolePrefix + " FAILED: " + e.Message);
				Console.WriteLine(consolePrefix + e.StackTrace);
			}
			finally
			{
				Console.WriteLine();
				Console.WriteLine("-----");
				Console.WriteLine();
			}
		}
	}
}