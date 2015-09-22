using System;
using System.Linq.Expressions;

namespace Cirrus.Core.Workflow.Processes
{
	//A single step implementation of an executable
	//We take in an Expression<> so that they can be serialised and compiled potentially by someone on a different machine
	public class Step<TIn, TOut> : IExecute
	{
		public Step(Expression<Func<TIn, TOut>> step)
		{
			_step = step;
		}

		private Expression<Func<TIn, TOut>> _step;

		public TOut Execute(TIn input)
		{
			return _step.Compile()(input);
		}

		public object Execute(object input)
		{
			if (input is TIn)
			{
				return Execute((TIn)input);
			}
			else
			{
				throw new ArgumentException(nameof(input), string.Format("The supplied input type was not {0}", nameof(TIn)));
			}
		}
	}
}