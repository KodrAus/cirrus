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
			runner.Run(() => Assert.Process_Can_Execute_Another_Process_As_A_Step());
			runner.Run(() => Assert.Process_Can_Execute_As_IProcessStep());
			runner.Run(() => Assert.Process_Throws_NullReferenceException_If_Step_Is_Not_Supplied_When_Running_As_IProcessStep());
			runner.Run(() => Assert.Process_Throws_NullReferenceException_If_Arg_Is_Not_Supplied_When_Running_As_Process());

			runner.Run(() => Assert.ICommandInput_Takes_Generic_Argument());
			runner.Run(() => Assert.IQueryInput_Takes_Generic_Argument());
			runner.Run(() => Assert.IQueryOutput_Takes_Generic_Argument());
			
			Console.WriteLine("Done");
		}
	}

	//Tests
	static class Assert
	{
		//Command
		public static bool ICommandInput_Takes_Generic_Argument()
		{
			throw new NotImplementedException();
		}

		//Query
		public static bool IQueryInput_Takes_Generic_Argument()
		{
			throw new NotImplementedException();
		}

		public static bool IQueryOutput_Takes_Generic_Argument()
		{
			throw new NotImplementedException();
		}

		//Process
		public static bool IProcessStepInput_Takes_Generic_Argument()
		{
			string inputValue = "my string";
			var input = new TestProcessStepInput1(inputValue);

			return ((ProcessInputBase<string>)input).Input == inputValue;
		}

		public static bool IProcessStepOutput_Takes_Generic_Argument()
		{
			string resultValue = "my string";
			var output = new TestProcessStepOutput1(resultValue);

			return ((ProcessOutputBase<string>)output).Result == resultValue;
		}

		public static bool Process_Takes_Generic_ProcessStep_Args()
		{
			string input = "my input";
			var process = new Process<TestProcessStepInput1, TestProcessStepOutput1>(new TestProcessStepInput1(input));
			
			var result = process.ExecuteStep(new TestProcessStep1()).Result;

			return result.Result == input;
		}

		public static bool Process_Returns_Unravelled_Process_As_Result()
		{
			var process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStepInput1("my input"),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

		    var result = process.ExecuteStep(new TestProcessStep1()).Result;

		    return result is Process<TestProcessStepInput2, TestProcessStepOutput2>;
		}

		public static bool Process_Executes_All_Steps()
		{
			var process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStepInput1("my input"),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

		    var result = process.Execute(new TestProcessStep1(), new TestProcessStep2()).Result;

		    return result is TestProcessStepOutput2;
		}

		public static bool Process_Can_Execute_Another_Process_As_A_Step()
		{
			var process = new Process
			<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2,
				TestProcessStepInput2, TestProcessStepOutput2
			>
			(
				new TestProcessStepInput1("my input"),
				
				(out1) => new TestProcessStepInput2(out1.Result),
				(out2) => new TestProcessStepInput2(out2.Result)
			);

			//Needs to take constructor with steps
			var subProcess = new Process<
				TestProcessStepInput2, TestProcessStepOutput2, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStep2(),
				new TestProcessStep2(),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

			var result = process.Execute(new TestProcessStep1(), subProcess, new TestProcessStep2()).Result;

			return result is TestProcessStepOutput2;
		}

		public static bool Process_Can_Execute_As_IProcessStep()
		{
			IProcessStep<TestProcessStepInput1, TestProcessStepOutput2> process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStep1(),
				new TestProcessStep2(),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

		    var result = process.Execute(new TestProcessStepInput1("my input")).Result;

		    return result is TestProcessStepOutput2;
		}

		public static bool Process_Throws_NullReferenceException_If_Step_Is_Not_Supplied_When_Running_As_IProcessStep()
		{
			throw new NotImplementedException();
		}

		public static bool Process_Throws_NullReferenceException_If_Arg_Is_Not_Supplied_When_Running_As_Process()
		{
			throw new NotImplementedException();
		}
	}
}