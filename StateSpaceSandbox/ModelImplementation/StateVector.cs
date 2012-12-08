using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// State vector implementation
    /// </summary>
    internal sealed class StateVector : AbstractVector, IStateVector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlVector" /> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public StateVector(int length)
            : base(length)
        {
        }

        /// <summary>
        /// Adds another control vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The other vector</param>
        /// <returns>IStateVector.</returns>
        public IStateVector Add(ISimulationTime simulationTime, IStateVector other)
        {
            IVector result = new StateVector(Length);
            Add(simulationTime, other, ref result);
            return (IStateVector)result;
        }

        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add</param>
        public void AddInPlace(ISimulationTime simulationTime, IStateVector other)
        {
            base.AddInPlace(simulationTime, other);
        }

        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add</param>
        /// <param name="output">The output.</param>
        public void Add(ISimulationTime simulationTime, IStateVector other, ref IStateVector output)
        {
            IVector result = output;
            Add(simulationTime, other, ref result);
        }
    }
}
