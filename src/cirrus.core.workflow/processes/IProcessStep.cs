using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Processes
{
	public interface IProcessStep
	{
		
	}

	public interface IProcessStep<TIn, TOut> : IProcessStep
		where TIn : class, IProcessInput
		where TOut : class, IProcessOutput
	{
		Task<TOut> Execute(TIn arg);
	}
}