using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a matrix
    /// </summary>
    public interface IMatrix
    {
        /// <summary>
        /// Gets the number of columns
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Gets the number of rows
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>IValueProvider.</returns>
        double GetValue(int row, int column);

        /// <summary>
        /// Gets the value provider.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>IValueProvider.</returns>
        IValueProvider GetValueProvider(int row, int column);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        void SetValue(int row, int column, IValueProvider value);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        void SetValue(int row, int column, double value);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="valueFunction">The value.</param>
        void SetValue(int row, int column, Func<ISimulationTime, double> valueFunction);
    }
}
