using Cirrus.Core.Workflow;
using System;
using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Tests
{
	public class TestCommandInput1 : CommandInputBase<string>
	{
		public TestCommandInput1(string input)
			: base(input)
		{

		}
	}

	//Just to highlight that there's a difference between the input and output classes in our test
	public class TestCommandOutput1 : CommandOutputBase<string>
	{
		public TestCommandOutput1(string result)
			: base(result)
		{

		}
	}

	public class TestCommand1 : ICommand<TestCommandInput1, TestCommandOutput1>
	{
		public Task<TestCommandOutput1> Execute(TestCommandInput1 arg)
		{
			Console.WriteLine("Executing with " + arg.Input);

			return Task.FromResult(new TestCommandOutput1(arg.Input));
		}
	}
}