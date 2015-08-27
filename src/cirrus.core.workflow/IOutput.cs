namespace Cirrus.Core.Workflow
{
	public interface IOutput
	{

	}

	public interface IOutput<T> : IOutput
	{
		T Result { get; }

		//TODO: Errors n stuff
	}

	public class OutputBase<T> : IOutput<T>
	{
		public OutputBase(T result)
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