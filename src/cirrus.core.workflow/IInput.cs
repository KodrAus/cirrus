namespace Cirrus.Core.Workflow
{
	public interface IInput
	{

	}

	public interface IInput<T> : IInput
	{
		T Input { get; }
	}

	public class InputBase<T> : IInput<T>
	{
		public InputBase(T input)
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