using Cirrus.Core.Workflow;
using System;
using System.Threading.Tasks;
using Cirrus.Core.Workflow.Processes;

namespace Cirrus.Core.Workflow.Tests.ProcessFixtures
{
	public class TestProcessStepInput1 : ProcessInputBase<string>
	{
		public TestProcessStepInput1(string input)
			: base(input)
		{

		}
	}

	//Just to highlight that there's a difference between the input and output classes in our test
	public class TestProcessStepOutput1 : ProcessOutputBase<string>
	{
		public TestProcessStepOutput1(string result)
			: base(result)
		{

		}
	}

	public class TestProcessStep1 : IProcessStep<TestProcessStepInput1, TestProcessStepOutput1>
	{
		public Task<TestProcessStepOutput1> Execute(TestProcessStepInput1 arg)
		{
			Console.WriteLine("Executing with " + arg.Input);

			return Task.FromResult(new TestProcessStepOutput1(arg.Input));
		}
	}
}