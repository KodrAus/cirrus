using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Cirrus.Core.Workflow.Processes;
using Xunit;

namespace Cirrus.Core.Workflow.Tests
{
	public class ProcessTests
	{
		//Verify that custom methods can be used in the expression
		public int MyStepMethod(string input)
		{
			return Convert.ToInt32(input);
		}

		[Fact]
		public void Process_Takes_Steps_And_Executes()
		{
			//Try create a process using a simple factory
			var process = Process.New(
				new Step<string, int>(input => MyStepMethod(input)),
				new Step<int, int>(input => input + 1),
				new Step<int, string>(input => input.ToString()),
				new Step<string, string>(input => "result is: " + input)
			);

			var result = process.Execute("1");

			Assert.Equal("result is: 2", result);
		}

		[Fact]
		public void Process_Takes_Process_As_Step_And_Executes()
		{
			//Try to create a process that takes another process as a sub step
			var process = Process.New(
				new Step<string, int>(input => Convert.ToInt32(input)),
				new Step<int, int>(input => input + 1),
				Process.New(
					new Step<int, string>(input => input.ToString() + " with extra bits")
				),
				new Step<string, string>(input => "result is: " + input)
			);

			var result = process.Execute("1");

			Assert.Equal("result is: 2 with extra bits", result);
		}

		[Fact]
		public void Process_Returns_PartialResult_When_Computation_Is_Not_Complete()
		{
			//Create a process that contains more than a single step
			var process = Process.New(
				new Step<int, int>(input => input + 1),
				new Step<int, int>(input => input + 3)
			);

			var result = process.ExecuteStep(1);

			Assert.NotNull(result.Process);
			Assert.Equal(2, result.Result);
		}

		[Fact]
		public void Process_Returns_Final_Result_When_Computation_Is_Complete()
		{
			var process = Process.New(
				new Step<int, int>(input => input + 1),
				new Step<int, int>(input => input + 3)
			);

			var result = process.ExecuteStep(1);

			//Run the rest of the computation, ensure there is no process left to run afterwards
			result = result.ExecuteStep();

			Assert.Null(result.Process);
		}

		[Fact]
		public void Process_Throws_If_Input_Type_To_Step_Is_Not_Correct()
		{
			bool failed = false;

			var process = Process.New(
				new Step<int, string>(input => input.ToString()),
				new Step<int, string>(input => input.ToString())
			);

			try
			{
				process.Execute(1);
			}
			catch (ArgumentException)
			{
				failed = true;
			}

			Assert.True(failed);
		}
	}
}