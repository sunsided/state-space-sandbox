namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a state vector ("x")
    /// </summary>
    public interface IStateVector : IVector
    {
        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add</param>
        /// <returns>IStateVector.</returns>
        IStateVector Add(ISimulationTime simulationTime, IStateVector other);

        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add</param>
        void AddInPlace(ISimulationTime simulationTime, IStateVector other);

        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add</param>
        /// <param name="output">The output.</param>
        void Add(ISimulationTime simulationTime, IStateVector other, ref IStateVector output);
    }
}
