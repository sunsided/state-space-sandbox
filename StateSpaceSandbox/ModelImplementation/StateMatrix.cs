using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// State matrix implementation
    /// </summary>
    internal sealed class StateMatrix : AbstractMatrix, IStateMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMatrix" /> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public StateMatrix(int rows, int columns)
            : base(rows, columns)
        {
        }

        /// <summary>
        /// Transforms the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>IStateVector.</returns>
        public IStateVector Transform(ISimulationTime simulationTime, IStateVector vector)
        {
            IVector result = new StateVector(Rows);
            Transform(simulationTime, vector, ref result);
            return (IStateVector)result;
        }

        /// <summary>
        /// Transforms the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="output">The output.</param>
        public void Transform(ISimulationTime simulationTime, IStateVector vector, ref IStateVector output)
        {
            IVector result = output;
            Transform(simulationTime, vector, ref result);
        }
    }
}
