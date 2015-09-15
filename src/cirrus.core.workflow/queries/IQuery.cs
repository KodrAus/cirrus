using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Queries
{
	public interface IQuery : IAgent
	{

	}

	public interface IQuery<TInput, TOutput> : IQuery
		where TInput : class, IQueryInput
		where TOutput : class, IQueryOutput
	{
		Task<TOutput> Query(TInput query);
	}
}