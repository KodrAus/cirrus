using Cirrus.Core.Workflow;
using System;
using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Tests
{
	public class TestCommandInput2 : CommandInputBase<object>
	{
		public TestCommandInput2(object input)
			: base(input)
		{

		}
	}

	//Just to highlight that there's a difference between the input and output classes in our test
	public class TestCommandOutput2 : CommandOutputBase<object>
	{
		public TestCommandOutput2(object result)
			: base(result)
		{

		}
	}

	public class TestCommand2 : ICommand<TestCommandInput2, TestCommandOutput2>
	{
		public Task<TestCommandOutput2> Execute(TestCommandInput2 arg)
		{
			Console.WriteLine("Executing with " + arg.Input);

			return Task.FromResult(new TestCommandOutput2(arg.Input));
		}
	}
}