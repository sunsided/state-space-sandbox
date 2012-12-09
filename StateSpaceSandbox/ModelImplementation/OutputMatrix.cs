using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// Output matrix implementation
    /// </summary>
    internal sealed class OutputMatrix : AbstractMatrix, IOutputMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMatrix" /> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public OutputMatrix(int rows, int columns)
            : base(rows, columns)
        {
        }

        /// <summary>
        /// Transforms the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>IStateVector.</returns>
        public IOutputVector Transform(IStateVector vector)
        {
            IWritableVector result = new OutputVector(Rows);
            Transform(vector, ref result);
            return (IOutputVector)result;
        }

        /// <summary>
        /// Transforms the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="output">The output.</param>
        public void Transform(IStateVector vector, ref IOutputVector output)
        {
            IWritableVector result = output;
            Transform(vector, ref result);
        }
    }
}
