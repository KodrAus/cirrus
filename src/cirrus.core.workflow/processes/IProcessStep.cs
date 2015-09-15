using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Processes
{
	public interface IProcessStep : IAgent
	{
		
	}

	public interface IProcessStep<TIn, TOut> : IProcessStep
		where TIn : class, IProcessInput
		where TOut : class, IProcessOutput
	{
		//This should involve queuing up the command and returning early to continue later
		Task<TOut> Execute(TIn arg);
	}
}