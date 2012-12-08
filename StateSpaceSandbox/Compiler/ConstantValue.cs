using System;
using System.Linq.Expressions;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.Compiler
{
    /// <summary>
    /// Describes a constant value
    /// </summary>
    public sealed class ConstantValue : IValueProvider
    {
        /// <summary>
        /// The zero value
        /// </summary>
        public static readonly ConstantValue Zero = new ConstantValue(0);

        /// <summary>
        /// The zero value
        /// </summary>
        public static readonly ConstantValue One = new ConstantValue(1);

        /// <summary>
        /// Gets the value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantValue" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ConstantValue(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Determines if this constant is <code>1.0D</code>
        /// </summary>
        public bool IsOne
        {
            get { return Math.Abs(Value - 1.0D) <= Double.Epsilon; }
        }

        /// <summary>
        /// Determines if this constant is <code>0.0D</code>
        /// </summary>
        public bool IsZero
        {
            get { return Math.Abs(Value) <= Double.Epsilon; }
        }

        /// <summary>
        /// Gets the value
        /// </summary>
        public double GetValue(ISimulationTime time)
        {
            return Value;
        }

        /// <summary>
        /// Gets an expression that provides this item's value
        /// </summary>
        /// <returns>The expression</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Expression GetExpression()
        {
            return Expression.Constant(Value, typeof (double));
        }
    }
}
