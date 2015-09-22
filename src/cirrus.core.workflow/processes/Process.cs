using System.Linq;

namespace Cirrus.Core.Workflow.Processes
{
	//A collection of IExecutes that are chained to together to form a process computation
	//The Process is a tail-recursive structure, and runs through each step one at a time, threading a PartialResult through
	public class Process : IExecute
	{
		//Process Factory
		public static Process New(params IExecute[] steps)
		{
			Process p = new Process();
			p.Head = steps.First();

			if (steps.Count() > 1)
			{
				p.Tail = New(steps.Skip(1).ToArray());
			}

			return p;
		}

		//The next step to be executed
		public IExecute Head;

		//The rest of the process
		public Process Tail;

		//Execute just the next step and return the result, along with the rest of the process
		public PartialResult ExecuteStep(object input)
		{
			if (Tail != null)
			{
				return new PartialResult { Result = Head.Execute(input), Process = Tail };
			}
			else
			{
				return new PartialResult { Result = Head.Execute(input) };
			}
		}

		//Execute the complete process
		public object Execute(object input)
		{
			PartialResult res = ExecuteStep(input);
			return res.Execute();
		}
	}
}