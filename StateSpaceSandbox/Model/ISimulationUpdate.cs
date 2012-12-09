namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Interface for elements that can update during simulation
    /// </summary>
    public interface ISimulationUpdate
    {
        /// <summary>
        /// Updates the element
        /// </summary>
        /// <param name="time">The current time</param>
        void Update(ISimulationTime time);
    }
}
