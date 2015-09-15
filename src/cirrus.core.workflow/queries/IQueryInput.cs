namespace Cirrus.Core.Workflow.Queries
{
	public interface IQueryInput : IInput
	{

	}

	public interface IQueryInput<T> : IInput<T>, IQueryInput
	{

	}

	public class QueryInputBase<T> : InputBase<T>, IQueryInput<T>
	{
		public QueryInputBase(T input)
			: base(input)
		{

		}
	}
}