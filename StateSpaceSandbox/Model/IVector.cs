using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a vector
    /// </summary>
    public interface IVector
    {
        /// <summary>
        /// The vector's length
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="simulationTime">The simulation time.</param>
        /// <returns>IValueProvider.</returns>
        double GetValue(int index, ISimulationTime simulationTime);

        /// <summary>
        /// Gets the value provider.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>IValueProvider.</returns>
        IValueProvider GetValueProvider(int index);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        void SetValue(int index, IValueProvider value);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        void SetValue(int index, double value);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="valueFunction">The value.</param>
        void SetValue(int index, Func<ISimulationTime, double> valueFunction);
    }
}
