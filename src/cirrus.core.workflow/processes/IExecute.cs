namespace Cirrus.Core.Workflow.Processes
{
	//Represents a general component of an execution
	public interface IExecute
	{
		object Execute(object input);
	}
}