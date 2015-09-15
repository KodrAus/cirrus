using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Commands
{
	public interface ICommand : IAgent
	{

	}

	public interface ICommand<T> : ICommand
		where T : class, ICommandInput
	{
		Task Execute(T arg);
	}
}