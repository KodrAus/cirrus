using System;
using System.Threading.Tasks;
using Cirrus.Test;

namespace Cirrus.Core.Workflow.Tests
{
	//Workflow<TIn1, TOut1, TIn2, TOut2, TIn3, TOut3 ...>
	//When executing, unravel the workflow...
	//Workflow<TIn1, TOut1, TIn2, TOut2> -> Workflow<TIn2, TOut2> -> TOut2
	//The idea is that we can chain up our ICommand<TIn, TOut>, and execute the workflow over all the services in our cluster
	class Program
	{
		//Runner
		public static void Main(string[] args)
		{
			var runner = new TestRunner();

			Console.WriteLine("Testing...");

			runner.Run(() => Assert.Workflow_Takes_Generic_Command_Args());
			runner.Run(() => Assert.Workflow_Returns_Unravelled_Workflow_As_Result());

			Console.WriteLine("Done");
		}
	}

	//Tests
	static class Assert
	{
		public static bool Workflow_Takes_Generic_Command_Args()
		{
			string input = "my input";
			var workflow = new WorkflowContainer<TestCommandInput1, TestCommandOutput1>(new TestCommandInput1 { InArg = input });
			
			var result = workflow.Execute(new TestCommand1()).Result;

			return result.OutArg2 == input;
		}

		public static bool Workflow_Returns_Unravelled_Workflow_As_Result()
		{
			var workflow = new WorkflowContainer<TestCommandInput1, TestCommandOutput1, 
												 TestCommandInput2, TestCommandOutput2>(
												 new TestCommandInput1 { InArg = "my input" },
												 (out1) => new TestCommandInput2 { InArg = out1.OutArg1 });

		    var result = workflow.Execute(new TestCommand1()).Result;

		    return result is WorkflowContainer<TestCommandInput2, TestCommandOutput2>;
		}
	}
}