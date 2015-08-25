using Cirrus.Core.Workflow;
using System;
using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Tests
{
	public class TestCommandInput2
	{
		public object InArg { get; set; }
	}

	public class TestCommandOutput2
	{
		public object OutArg { get; set; }
	}

	public class TestCommand2 : ICommand<TestCommandInput2, TestCommandOutput2>
	{
		public Task<TestCommandOutput2> Execute(TestCommandInput2 arg)
		{
			Console.WriteLine("Executing with " + arg.InArg.GetType().Name);

			return Task.FromResult(new TestCommandOutput2 { OutArg = arg.InArg });
		}
	}
}