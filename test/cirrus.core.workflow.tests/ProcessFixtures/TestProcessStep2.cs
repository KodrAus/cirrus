using Cirrus.Core.Workflow;
using System;
using System.Threading.Tasks;
using Cirrus.Core.Workflow.Processes;

namespace Cirrus.Core.Workflow.Tests.ProcessFixtures
{
	public class TestProcessStepInput2 : ProcessInputBase<object>
	{
		public TestProcessStepInput2(object input)
			: base(input)
		{

		}
	}

	//Just to highlight that there's a difference between the input and output classes in our test
	public class TestProcessStepOutput2 : ProcessOutputBase<object>
	{
		public TestProcessStepOutput2(object result)
			: base(result)
		{

		}
	}

	public class TestProcessStep2 : IProcessStep<TestProcessStepInput2, TestProcessStepOutput2>
	{
		public Task<TestProcessStepOutput2> Execute(TestProcessStepInput2 arg)
		{
			Console.WriteLine("Executing with " + arg.Input);

			return Task.FromResult(new TestProcessStepOutput2(arg.Input));
		}
	}
}