using System;
using System.Threading.Tasks;
using Cirrus.Core.Workflow.Processes;
using Cirrus.Core.Workflow.Tests.ProcessFixtures;
using Xunit;

namespace Cirrus.Core.Workflow.Tests
{
	public class ProcessTests
	{
		[Fact]
		public void IProcessStepInput_Takes_Generic_Argument()
		{
			string inputValue = "my string";
			var input = new TestProcessStepInput1(inputValue);

			Assert.Equal(((ProcessInputBase<string>)input).Input, inputValue);
		}

		[Fact]
		public async Task IProcessStepOutput_Takes_Generic_Argument()
		{
			string resultValue = "my string";
			var output = new TestProcessStepOutput1(resultValue);

			Assert.Equal(((ProcessOutputBase<string>)output).Result, resultValue);
		}

		[Fact]
		public async Task Process_Takes_Generic_ProcessStep_IO_Args()
		{
			string input = "my input";
			var process = new Process<TestProcessStepInput1, TestProcessStepOutput1>(new TestProcessStepInput1(input));
			
			var result = await process.ExecuteStep(new TestProcessStep1());

			Assert.Equal(result.Result, input);
		}

		[Fact]
		public async Task Process_Takes_Generic_ProcessStep()
		{
			string input = "my input";
			var process = new StrongProcess<TestProcessStep1, TestProcessStepInput1, TestProcessStepOutput1>(new TestProcessStepInput1(input));

			var result = await process.ExecuteStep(new TestProcessStep1());

			Assert.Equal(result.Result, input);
		}

		[Fact]
		public async Task Process_Returns_Unravelled_Process_As_Result()
		{
			var process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStepInput1("my input"),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

		    var result = await process.ExecuteStep(new TestProcessStep1());

		    Assert.True(result is Process<TestProcessStepInput2, TestProcessStepOutput2>);
		}

		[Fact]
		public async Task Process_Executes_All_Steps()
		{
			var process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStepInput1("my input"),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

		    var result = await process.Execute(new TestProcessStep1(), new TestProcessStep2());

		    Assert.True(result is TestProcessStepOutput2);
		}

		[Fact]
		public async Task Process_Can_Execute_Another_Process_As_A_Step()
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

			var result = await process.Execute(new TestProcessStep1(), subProcess, new TestProcessStep2());

			Assert.True(result is TestProcessStepOutput2);
		}

		[Fact]
		public async Task Process_Can_Execute_As_IProcessStep()
		{
			IProcessStep<TestProcessStepInput1, TestProcessStepOutput2> process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStep1(),
				new TestProcessStep2(),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

		    var result = await process.Execute(new TestProcessStepInput1("my input"));

		    Assert.True(result is TestProcessStepOutput2);
		}

		[Fact]
		public async Task Process_Throws_NullReferenceException_If_Step_Is_Not_Supplied_When_Running_As_IProcessStep()
		{
			//Build a process without supplying any steps
			//This means we can't execute as a step itself because there's no way to supply those arguments
			IProcessStep<TestProcessStepInput1, TestProcessStepOutput2> process = new Process<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2>
			(
				new TestProcessStepInput1("my input"),
				(out1) => new TestProcessStepInput2(out1.Result)
			);

			//Try to run the process without the steps necessary
			bool failed = false;
			try
			{
			    var result = await process.Execute(new TestProcessStepInput1("my input"));
			}
			catch
			{
				failed = true;
			}

		    Assert.True(failed);
		}

		[Fact]
		public async Task Process_Throws_NullReferenceException_If_Arg_Is_Not_Supplied_When_Running_As_Process()
		{
			var process = new Process
			<
				TestProcessStepInput1, TestProcessStepOutput1, 
				TestProcessStepInput2, TestProcessStepOutput2
			>
			(
				new TestProcessStep1(),
				new TestProcessStep2(),
				
				(out1) => new TestProcessStepInput2(out1.Result)
			);

			bool failed = false;
			try
			{
				var result = await process.ExecuteStep(new TestProcessStep1());
			}
			catch
			{
				failed = true;
			}

			Assert.True(failed);
		}
	}
}