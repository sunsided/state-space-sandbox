namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes the output vector
    /// </summary>
    public interface IOutputVector : IVector
    {
        /// <summary>
        /// Adds a control vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add</param>
        /// <returns>IOutputVector.</returns>
        IOutputVector Add(ISimulationTime simulationTime, IOutputVector other);

        /// <summary>
        /// Adds a control vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add</param>
        void AddInPlace(ISimulationTime simulationTime, IOutputVector other);

        /// <summary>
        /// Adds a control vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add</param>
        /// <param name="output">The output.</param>
        void Add(ISimulationTime simulationTime, IOutputVector other, ref IOutputVector output);
    }
}
