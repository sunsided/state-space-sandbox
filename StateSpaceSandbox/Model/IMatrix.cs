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
        /// Provides access to the matrix elements
        /// </summary>
        /// <param name="column">The column index.</param>
        /// <param name="row">The row index.</param>
        /// <returns>The value</returns>
        double this[int column, int row] { get; set; }
    }
}
