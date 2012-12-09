using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a vector
    /// </summary>
    public interface IVector : ISimulationUpdate
    {
        /// <summary>
        /// The vector's length
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the value provider.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>IValueProvider.</returns>
        IValueProvider GetValueProvider(int index);
    }

    /// <summary>
    /// Describes a vector
    /// </summary>
    public interface IDynamicVector : IReadableVector, IWritableVector
    {
    }

    /// <summary>
    /// Describes a readable vector
    /// </summary>
    public interface IReadableVector : IVector
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>IValueProvider.</returns>
        double GetValue(int index);
    }

    /// <summary>
    /// Describes a vector
    /// </summary>
    public interface IWritableVector : IVector
    {
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
