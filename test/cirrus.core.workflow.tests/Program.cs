using System;
using System.Threading.Tasks;
using Cirrus.Test;
using Cirrus.Core.Workflow.Processes;

namespace Cirrus.Core.Process.Tests
{
	//Process<TIn1, TOut1, TIn2, TOut2, TIn3, TOut3 ...>
	//When executing, unravel the Process...
	//Process<TIn1, TOut1, TIn2, TOut2> -> Process<TIn2, TOut2> -> TOut2
	//The idea is that we can chain up our IProcessStep<TIn, TOut>, and execute the Process over all the services in our cluster
	class Program
	{
		//Runner
		public static void Main(string[] args)
		{
			var runner = new TestRunner();

			Console.WriteLine("Testing...");

			runner.Run(() => Assert.IProcessStepInput_Takes_Generic_Argument());
			runner.Run(() => Assert.IProcessStepOutput_Takes_Generic_Argument());

			runner.Run(() => Assert.Process_Takes_Generic_ProcessStep_Args());
			runner.Run(() => Assert.Process_Returns_Unravelled_Process_As_Result());

			Console.WriteLine("Done");
		}
	}

	//Tests
	static class Assert
	{
		public static bool IProcessStepInput_Takes_Generic_Argument()
		{
			throw new NotImplementedException();
		}

		public static bool IProcessStepOutput_Takes_Generic_Argument()
		{
			throw new NotImplementedException();
		}

		public static bool Process_Takes_Generic_ProcessStep_Args()
		{
			string input = "my input";
			var Process = new Process<TestProcessStepInput1, TestProcessStepOutput1>(new TestProcessStepInput1(input));
			
			var result = Process.ExecuteStep(new TestProcessStep1()).Result;

			return result.Result == input;
		}

		public static bool Process_Returns_Unravelled_Process_As_Result()
		{
			var Process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStepInput1("my input"),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

		    var result = Process.ExecuteStep(new TestProcessStep1()).Result;

		    return result is Process<TestProcessStepInput2, TestProcessStepOutput2>;
		}

		public static bool Process_Executes_All_Steps()
		{
			var Process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStepInput1("my input"),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

		    var result = Process.Execute(new TestProcessStep1(), new TestProcessStep2()).Result;

		    return result is TestProcessStepOutput2;
		}
	}
}