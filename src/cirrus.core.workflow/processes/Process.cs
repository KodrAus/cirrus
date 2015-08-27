using System;
using System.Threading.Tasks;

namespace Cirrus.Core.Workflow.Processes
{
	//TODO: Investigate ways of automating the loop through processes
	//This code is currently very manual and clunky, but if it remains
	//fairly untouched 'framework' code then we can probably get away with it

	//TODO: Standardise repeat logic (error checks etc)
	public class Process<TIn, TOut> : IProcess, IProcessStep<TIn, TOut>
		where TIn : class, IProcessInput
		where TOut : class, IProcessOutput
	{
		public Process(TIn arg)
		{
			_arg = arg;
		}

		public Process(IProcessStep<TIn, TOut> step)
		{
			_step = step;
		}

		private TIn _arg;
		private IProcessStep<TIn, TOut> _step;

		public async Task<TOut> ExecuteStep(IProcessStep<TIn, TOut> process)
		{
			if (_arg == null)
			{
				throw new NullReferenceException(string.Format("You must provide an argument of type {0} in order to execute with supplied IProcessStep", nameof(TIn))); 
			}

			return await process.Execute(_arg);
		}

		public async Task<TOut> Execute(IProcessStep<TIn, TOut> process)
		{
			return await ExecuteStep(process);
		}

		public async Task<TOut> Execute(TIn arg)
		{
			_arg = arg;

			if (_step == null)
			{
				throw new NullReferenceException("You must provide an IProcessStep in order to execute with supplied argument");
			}

			return await Execute(_step);
		}
	}

	public class Process<TIn1, TOut1, TIn2, TOut2> : IProcess, IProcessStep<TIn1, TOut2>
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

		public Process(IProcessStep<TIn1, TOut1> step1, IProcessStep<TIn2, TOut2> step2, Func<TOut1, TIn2> thread1)
			: this(null, thread1)
		{
			_step1 = step1;
			_step2 = step2;
		}

		private TIn1 _arg;

		private IProcessStep<TIn1, TOut1> _step1; 
		private IProcessStep<TIn2, TOut2> _step2;

		private Func<TOut1, TIn2> _thread1;

		public async Task<Process<TIn2, TOut2>> ExecuteStep(IProcessStep<TIn1, TOut1> process)
		{
			if (_arg == null)
			{
				throw new NullReferenceException(string.Format("You must provide an argument of type {0} in order to execute with supplied IProcessStep", nameof(TIn1))); 
			}

			var result = await process.Execute(_arg);

			return new Process<TIn2, TOut2>(_thread1(result));
		}

		public async Task<TOut2> Execute(IProcessStep<TIn1, TOut1> process1, IProcessStep<TIn2, TOut2> process2)
		{
			var result = await process1.Execute(_arg);

			return await process2.Execute(_thread1(result));
		}

		public async Task<TOut2> Execute(TIn1 arg)
		{
			_arg = arg;

			if (_step1 == null || _step2 == null)
			{
				throw new NullReferenceException("You must provide an IProcessStep in order to execute with supplied argument");
			}

			return await Execute(_step1, _step2);
		}
	}

	public class Process<TIn1, TOut1, TIn2, TOut2, TIn3, TOut3> : IProcess, IProcessStep<TIn1, TOut3>
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

		public Process(IProcessStep<TIn1, TOut1> step1, IProcessStep<TIn2, TOut2> step2, IProcessStep<TIn3, TOut3> step3, Func<TOut1, TIn2> thread1, Func<TOut2, TIn3> thread2)
			: this(null, thread1, thread2)
		{
			_step1 = step1;
			_step2 = step2;
			_step3 = step3;
		}

		private TIn1 _arg;

		private IProcessStep<TIn1, TOut1> _step1; 
		private IProcessStep<TIn2, TOut2> _step2;
		private IProcessStep<TIn3, TOut3> _step3;

		private Func<TOut1, TIn2> _thread1;
		private Func<TOut2, TIn3> _thread2;

		public async Task<Process<TIn2, TOut2, TIn3, TOut3>> ExecuteStep(IProcessStep<TIn1, TOut1> process)
		{
			if (_arg == null)
			{
				throw new NullReferenceException(string.Format("You must provide an argument of type {0} in order to execute with supplied IProcessStep", nameof(TIn1))); 
			}

			var result = await process.Execute(_arg);

			return new Process<TIn2, TOut2, TIn3, TOut3>(_thread1(result), _thread2);
		}

		public async Task<TOut3> Execute(IProcessStep<TIn1, TOut1> process1, IProcessStep<TIn2, TOut2> process2, IProcessStep<TIn3, TOut3> process3)
		{
			var result1 = await process1.Execute(_arg);
			var result2 = await process2.Execute(_thread1(result1));

			return await process3.Execute(_thread2(result2));
		}

		public async Task<TOut3> Execute(TIn1 arg)
		{
			_arg = arg;

			if (_step1 == null || _step2 == null || _step3 == null)
			{
				throw new NullReferenceException("You must provide an IProcessStep in order to execute with supplied argument");
			}

			return await Execute(_step1, _step2, _step3);
		}
	}
}