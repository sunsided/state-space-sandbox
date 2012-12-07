using System;
using System.Linq.Expressions;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// Describes a constant value
    /// </summary>
    public sealed class ConstantValue : IExpressionProvider
    {
        /// <summary>
        /// Gets the value
        /// </summary>
        public double Value { get; private set; }

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
