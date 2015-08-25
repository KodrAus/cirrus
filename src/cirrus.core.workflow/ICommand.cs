using System.Threading.Tasks;

namespace Cirrus.Core.Workflow
{
	public interface ICommand
	{

	}

	public interface ICommand<T> : ICommand
	{
		Task Execute(T arg);
	}

	public interface ICommand<TIn, TOut> : ICommand
	{
		Task<TOut> Execute(TIn arg);
	}
}