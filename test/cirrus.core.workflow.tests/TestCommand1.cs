using Cirrus.Core.Workflow;
using System;
using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Tests
{
	public class TestCommandInput1
	{
		public string InArg { get; set; }
	}

	//Just to highlight that there's a difference between the input and output classes in our test
	public class TestCommandOutput1
	{
		public string OutArg1 { get; set; }
		public string OutArg2 { get; set; }
	}

	public class TestCommand1 : ICommand<TestCommandInput1, TestCommandOutput1>
	{
		public Task<TestCommandOutput1> Execute(TestCommandInput1 arg)
		{
			Console.WriteLine("Executing with " + arg.InArg);

			return Task.FromResult(new TestCommandOutput1 { OutArg1 = Guid.NewGuid().ToString(), OutArg2 = arg.InArg });
		}
	}
}