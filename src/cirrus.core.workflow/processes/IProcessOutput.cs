namespace Cirrus.Core.Workflow.Processes
{
	public interface IProcessOutput : IOutput
	{

	}

	public interface IProcessOutput<T> : IOutput<T>, IProcessOutput
	{

	}

	public class ProcessOutputBase<T> : OutputBase<T>, IProcessOutput<T>
	{
		public ProcessOutputBase(T result)
			: base(result)
		{

		}
	}
}