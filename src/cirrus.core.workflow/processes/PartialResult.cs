namespace Cirrus.Core.Workflow.Processes
{
	//Represents a partially completed Process
	public class PartialResult
	{
		public object Result;
		public Process Process;

		public object Execute()
		{
			//Execute the Process if it is not null, or return the result
			return Process != null ?
				Process.Execute(Result) :
				Result;
		}

		public PartialResult ExecuteStep()
		{
			//Execute a step in the Process if it is not null, or return this
			return Process != null ?
				Process.ExecuteStep(Result) :
				this;
		}
	}
}