using System;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.Compiler
{
    /// <summary>
    /// Describes a runtime-evaluated value
    /// </summary>
    public sealed class RuntimeValue : IValueProvider
    {
        /// <summary>
        /// Gets the value
        /// </summary>
        public Func<ISimulationTime, double> Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantValue" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public RuntimeValue(Func<ISimulationTime, double> value)
        {
            Value = value;
        }
        
        /// <summary>
        /// Gets the value
        /// </summary>
        public double GetValue(ISimulationTime time)
        {
            return Value(time);
        }
    }
}
