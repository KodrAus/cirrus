namespace Cirrus.Core.Workflow.Queries
{
	public interface IQueryOutput : IOutput
	{
		
	}

	public interface IQueryOutput<T> : IOutput<T>, IQueryOutput
	{
		
	}

	public class QueryOutputBase<T> : OutputBase<T>, IQueryOutput<T>
	{
		public QueryOutputBase(T input)
			: base(input)
		{
			
		}
	}
}