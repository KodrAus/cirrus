namespace Cirrus.Core.Workflow.Commands
{
	public interface ICommandOutput : IOutput
	{

	}

	public interface ICommandOutput<T> : ICommandOutput, IOutput<T>
	{

	}

	public class CommandOutputBase<T> : OutputBase<T>, ICommandOutput<T>
	{
		public CommandOutputBase(T result)
			:base(result)
		{

		}
	}
}