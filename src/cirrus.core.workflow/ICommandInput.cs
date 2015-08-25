namespace Cirrus.Core.Workflow
{
	public interface ICommandInput
	{

	}

	public interface ICommandInput<T> : ICommandInput
	{
		T Input { get; }
	}

	public class CommandInputBase<T> : ICommandInput<T>
	{
		public CommandInputBase(T input)
		{
			_input = input;
		}

		protected T _input;
		public T Input
		{
			get
			{
				return _input;
			}
		}
	}
}