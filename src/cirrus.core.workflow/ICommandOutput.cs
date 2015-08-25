namespace Cirrus.Core.Workflow
{
	public interface ICommandOutput
	{

	}

	public interface ICommandOutput<T> : ICommandOutput
	{
		T Result { get; }

		//TODO: Errors n stuff
	}

	public class CommandOutputBase<T> : ICommandOutput<T>
	{
		public CommandOutputBase(T result)
		{
			_result = result;
		}

		protected T _result;
		public T Result
		{
			get
			{
				return _result;
			}
		}
	}
}