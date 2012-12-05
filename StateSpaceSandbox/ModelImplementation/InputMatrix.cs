using System;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// Input matrix implementation
    /// </summary>
    internal sealed class InputMatrix : AbstractMatrix, IInputMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputMatrix" /> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public InputMatrix(int rows, int columns) : base(rows, columns)
        {
        }

        /// <summary>
        /// Transforms the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>IStateVector.</returns>
        public IStateVector Transform(IControlVector vector)
        {
            IVector result = new StateVector(Rows);
            Transform(vector, ref result);
            return (IStateVector)result;
        }

        /// <summary>
        /// Transforms the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="output">The output.</param>
        public void Transform(IControlVector vector, ref IStateVector output)
        {
            IVector result = output;
            Transform(vector, ref result);
        }
    }
}
