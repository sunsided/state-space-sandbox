using System;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.Compiler
{
    /// <summary>
    /// Describes a constant value
    /// </summary>
    public sealed class VariableValue : IValueProvider
    {
        /// <summary>
        /// The zero value
        /// </summary>
        public static readonly VariableValue Zero = new VariableValue(0);

        /// <summary>
        /// The zero value
        /// </summary>
        public static readonly VariableValue One = new VariableValue(1);

        /// <summary>
        /// Gets the value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantValue" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public VariableValue(double value)
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
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetValue(double value)
        {
            Value = value;
        }
    }
}
