using System;
using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Processes
{
	//TODO: Investigate ways of automating the loop through processes
	//This code is currently very manual and clunky, but if it remains
	//fairly untouched 'framework' code then we can probably get away with it
	public class Process<TIn, TOut> : IProcess
		where TIn : class, IProcessInput
		where TOut : class, IProcessOutput
	{
		public Process(TIn arg)
		{
			_arg = arg;
		}

		private TIn _arg;

		public async Task<TOut> ExecuteStep(IProcessStep<TIn, TOut> process)
		{
			return await process.Execute(_arg);
		}

		public async Task<TOut> Execute(IProcessStep<TIn, TOut> process)
		{
			return await ExecuteStep(process);
		}
	}

	public class Process<TIn1, TOut1, TIn2, TOut2> : IProcess
		where TIn1 : class, IProcessInput
		where TOut1 : class, IProcessOutput
		where TIn2 : class, IProcessInput
		where TOut2 : class, IProcessOutput
	{
		public Process(TIn1 arg, Func<TOut1, TIn2> thread1)
		{
			_arg = arg;
			_thread1 = thread1;
		}

		private TIn1 _arg;

		private Func<TOut1, TIn2> _thread1;

		public async Task<Process<TIn2, TOut2>> ExecuteStep(IProcessStep<TIn1, TOut1> process)
		{
			var result = await process.Execute(_arg);

			return new Process<TIn2, TOut2>(_thread1(result));
		}

		public async Task<TOut2> Execute(IProcessStep<TIn1, TOut1> process1, IProcessStep<TIn2, TOut2> process2)
		{
			var result = await process1.Execute(_arg);

			return await process2.Execute(_thread1(result));
		}
	}

	public class Process<TIn1, TOut1, TIn2, TOut2, TIn3, TOut3> : IProcess
		where TIn1 : class, IProcessInput
		where TOut1 : class, IProcessOutput
		where TIn2 : class, IProcessInput
		where TOut2 : class, IProcessOutput
		where TIn3 : class, IProcessInput
		where TOut3 : class, IProcessOutput
	{
		public Process(TIn1 arg, Func<TOut1, TIn2> thread1, Func<TOut2, TIn3> thread2)
		{
			_arg = arg;

			_thread1 = thread1;
			_thread2 = thread2;
		}

		private TIn1 _arg;

		private Func<TOut1, TIn2> _thread1;
		private Func<TOut2, TIn3> _thread2;

		public async Task<Process<TIn2, TOut2, TIn3, TOut3>> ExecuteStep(IProcessStep<TIn1, TOut1> process)
		{
			var result = await process.Execute(_arg);

			return new Process<TIn2, TOut2, TIn3, TOut3>(_thread1(result), _thread2);
		}

		public async Task<TOut3> Execute(IProcessStep<TIn1, TOut1> process1, IProcessStep<TIn2, TOut2> process2, IProcessStep<TIn3, TOut3> process3)
		{
			var result1 = await process1.Execute(_arg);
			var result2 = await process2.Execute(_thread1(result1));

			return await process3.Execute(_thread2(result2));
		}
	}
}