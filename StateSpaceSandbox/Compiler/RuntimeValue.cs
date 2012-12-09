using System;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.Compiler
{
    /// <summary>
    /// Describes a runtime-evaluated value
    /// </summary>
    public sealed class RuntimeValue : IValueProvider, ISimulationUpdate
    {
        /// <summary>
        /// Gets the value
        /// </summary>
        public Func<ISimulationTime, double> ValueFunction { get; private set; }

        /// <summary>
        /// The value
        /// </summary>
        private double _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantValue" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public RuntimeValue(Func<ISimulationTime, double> value)
        {
            if (value == null) throw new ArgumentNullException("value", "Evaluation function must not null");

            ValueFunction = value;
        }
        
        /// <summary>
        /// Gets the value
        /// </summary>
        public double Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Updates the element
        /// </summary>
        /// <param name="time">The current time</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(ISimulationTime time)
        {
            _value = ValueFunction(time);
        }
    }
}
