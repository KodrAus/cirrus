namespace Cirrus.Core.Workflow.Commands
{
	public interface ICommandInput : IInput
	{

	}

	public interface ICommandInput<T> : ICommandInput, IInput<T>
	{

	}

	public class CommandInputBase<T> : InputBase<T>, ICommandInput<T>
	{
		public CommandInputBase(T input)
			: base(input)
		{
			
		}
	}
}