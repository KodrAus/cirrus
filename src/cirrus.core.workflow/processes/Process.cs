using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

//TODO: Figure out if it's possible to infer some generic arguments in the case of the StrongProcess<> classes
namespace Cirrus.Core.Workflow.Processes
{
	//Anonymous message-driven
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

	//Strongly-typed step-driven
	public class StrongProcess<TStep, TIn, TOut> : Process<TIn, TOut>
		where TStep : class, IProcessStep<TIn, TOut>
		where TIn : class, IProcessInput
		where TOut : class, IProcessOutput
	{
		public StrongProcess(TIn arg)
			:base(arg)
		{

		}

		public StrongProcess(TStep step)
			:base(step)
		{

		}
	}

	//Anonymous message-driven
	public class Process<TIn1, TOut1, TIn2, TOut2> : IProcess, IProcessStep<TIn1, TOut2>
		where TIn1 : class, IProcessInput
		where TOut1 : class, IProcessOutput
		where TIn2 : class, IProcessInput
		where TOut2 : class, IProcessOutput
	{
		public Process(TIn1 arg, Expression<Func<TOut1, TIn2>> thread1)
		{
			_arg = arg;
			_thread1 = thread1;
		}

		public Process(IProcessStep<TIn1, TOut1> step1, IProcessStep<TIn2, TOut2> step2, Expression<Func<TOut1, TIn2>> thread1)
			:this(null, thread1)
		{
			_step1 = step1;
			_step2 = step2;
		}

		private TIn1 _arg;

		private IProcessStep<TIn1, TOut1> _step1; 
		private IProcessStep<TIn2, TOut2> _step2;

		private Expression<Func<TOut1, TIn2>> _thread1;

		public async Task<Process<TIn2, TOut2>> ExecuteStep(IProcessStep<TIn1, TOut1> process)
		{
			if (_arg == null)
			{
				throw new NullReferenceException(string.Format("You must provide an argument of type {0} in order to execute with supplied IProcessStep", nameof(TIn1))); 
			}

			var result = await process.Execute(_arg);

			return new Process<TIn2, TOut2>(_thread1.Compile().Invoke(result));
		}

		public async Task<TOut2> Execute(IProcessStep<TIn1, TOut1> process1, IProcessStep<TIn2, TOut2> process2)
		{
			var result = await process1.Execute(_arg);

			return await process2.Execute(_thread1.Compile().Invoke(result));
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

	//Strongly-typed step-driven
	public class StrongProcess<TStep1, TIn1, TOut1, TStep2, TIn2, TOut2> : Process<TIn1, TOut1, TIn2, TOut2>
		where TStep1 : class, IProcessStep<TIn1, TOut1>
		where TIn1 : class, IProcessInput
		where TOut1 : class, IProcessOutput
		where TStep2 : class, IProcessStep<TIn2, TOut2>
		where TIn2 : class, IProcessInput
		where TOut2 : class, IProcessOutput
	{
		public StrongProcess(TIn1 arg, Expression<Func<TOut1, TIn2>> thread1)
			:base(arg, thread1)
		{

		}

		public StrongProcess(TStep1 step1, TStep2 step2, Expression<Func<TOut1, TIn2>> thread1)
			:base(step1, step2, thread1)
		{

		}
	}

	//Anonymous message-driven
	public class Process<TIn1, TOut1, TIn2, TOut2, TIn3, TOut3> : IProcess, IProcessStep<TIn1, TOut3>
		where TIn1 : class, IProcessInput
		where TOut1 : class, IProcessOutput
		where TIn2 : class, IProcessInput
		where TOut2 : class, IProcessOutput
		where TIn3 : class, IProcessInput
		where TOut3 : class, IProcessOutput
	{
		public Process(TIn1 arg, Expression<Func<TOut1, TIn2>> thread1, Expression<Func<TOut2, TIn3>> thread2)
		{
			_arg = arg;

			_thread1 = thread1;
			_thread2 = thread2;
		}

		public Process(IProcessStep<TIn1, TOut1> step1, IProcessStep<TIn2, TOut2> step2, IProcessStep<TIn3, TOut3> step3, Expression<Func<TOut1, TIn2>> thread1, Expression<Func<TOut2, TIn3>> thread2)
			:this(null, thread1, thread2)
		{
			_step1 = step1;
			_step2 = step2;
			_step3 = step3;
		}

		private TIn1 _arg;

		private IProcessStep<TIn1, TOut1> _step1; 
		private IProcessStep<TIn2, TOut2> _step2;
		private IProcessStep<TIn3, TOut3> _step3;

		private Expression<Func<TOut1, TIn2>> _thread1;
		private Expression<Func<TOut2, TIn3>> _thread2;

		public async Task<Process<TIn2, TOut2, TIn3, TOut3>> ExecuteStep(IProcessStep<TIn1, TOut1> process)
		{
			if (_arg == null)
			{
				throw new NullReferenceException(string.Format("You must provide an argument of type {0} in order to execute with supplied IProcessStep", nameof(TIn1))); 
			}

			var result = await process.Execute(_arg);

			return new Process<TIn2, TOut2, TIn3, TOut3>(_thread1.Compile().Invoke(result), _thread2);
		}

		public async Task<TOut3> Execute(IProcessStep<TIn1, TOut1> process1, IProcessStep<TIn2, TOut2> process2, IProcessStep<TIn3, TOut3> process3)
		{
			var result1 = await process1.Execute(_arg);
			var result2 = await process2.Execute(_thread1.Compile().Invoke(result1));

			return await process3.Execute(_thread2.Compile().Invoke(result2));
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

	//Strongly-typed step-driven
	public class StrongProcess<TStep1, TIn1, TOut1, TStep2, TIn2, TOut2, TStep3, TIn3, TOut3> : Process<TIn1, TOut1, TIn2, TOut2, TIn3, TOut3>
		where TStep1 : class, IProcessStep<TIn1, TOut1>
		where TIn1 : class, IProcessInput
		where TOut1 : class, IProcessOutput
		where TStep2 : class, IProcessStep<TIn2, TOut2>
		where TIn2 : class, IProcessInput
		where TOut2 : class, IProcessOutput
		where TStep3 : class, IProcessStep<TIn3, TOut3>
		where TIn3 : class, IProcessInput
		where TOut3 : class, IProcessOutput
	{
		public StrongProcess(TIn1 arg, Expression<Func<TOut1, TIn2>> thread1, Expression<Func<TOut2, TIn3>> thread2)
			:base(arg, thread1, thread2)
		{

		}

		public StrongProcess(TStep1 step1, TStep2 step2, TStep3 step3, Expression<Func<TOut1, TIn2>> thread1, Expression<Func<TOut2, TIn3>> thread2)
			:base(step1, step2, step3, thread1, thread2)
		{

		}
	}
}