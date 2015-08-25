using System.Threading.Tasks;

namespace Cirrus.Core.Workflow
{
	public interface ICommand
	{

	}

	public interface ICommand<T> : ICommand
		where T : class, ICommandInput
	{
		Task Execute(T arg);
	}

	public interface ICommand<TIn, TOut> : ICommand
		where TIn : class, ICommandInput
		where TOut : class, ICommandOutput
	{
		Task<TOut> Execute(TIn arg);
	}
}