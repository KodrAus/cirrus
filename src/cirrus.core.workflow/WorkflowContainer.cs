using System;
using System.Threading.Tasks;

namespace Cirrus.Core.Workflow
{
	public class WorkflowContainer<TIn, TOut>
	{
		public WorkflowContainer(TIn arg)
		{
			_arg = arg;
		}

		private TIn _arg;

		public async Task<TOut> Execute(ICommand<TIn, TOut> command)
		{
			return await command.Execute(_arg);
		}
	}

	public class WorkflowContainer<TIn1, TOut1, TIn2, TOut2>
	{
		public WorkflowContainer(TIn1 arg, Func<TOut1, TIn2> thread1)
		{
			_arg = arg;
			_thread1 = thread1;
		}

		private TIn1 _arg;

		private Func<TOut1, TIn2> _thread1;

		public async Task<WorkflowContainer<TIn2, TOut2>> Execute(ICommand<TIn1, TOut1> command)
		{
			var result = await command.Execute(_arg);

			return new WorkflowContainer<TIn2, TOut2>(_thread1(result));
		}
	}

	public class WorkflowContainer<TIn1, TOut1, TIn2, TOut2, TIn3, TOut3>
	{
		public WorkflowContainer(TIn1 arg, Func<TOut1, TIn2> thread1, Func<TOut2, TIn3> thread2)
		{
			_arg = arg;

			_thread1 = thread1;
			_thread2 = thread2;
		}

		private TIn1 _arg;

		private Func<TOut1, TIn2> _thread1;
		private Func<TOut2, TIn3> _thread2;

		public async Task<WorkflowContainer<TIn2, TOut2, TIn3, TOut3>> Execute(ICommand<TIn1, TOut1> command)
		{
			var result = await command.Execute(_arg);

			return new WorkflowContainer<TIn2, TOut2, TIn3, TOut3>(_thread1(result), _thread2);
		}
	}
}