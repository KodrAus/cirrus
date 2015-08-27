namespace Cirrus.Core.Workflow.Processes
{
	public interface IProcessInput : IInput
	{

	}

	public interface IProcessInput<T> : IInput<T>, IProcessInput
	{

	}

	public class ProcessInputBase<T> : InputBase<T>, IProcessInput<T>
	{
		public ProcessInputBase(T input)
			: base(input)
		{

		}
	}
}