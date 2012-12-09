using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Simulation related events
    /// </summary>
    public class StateEventArgs : SimulationEventArgs
    {
        /// <summary>
        /// Gets the state vector.
        /// </summary>
        /// <value>The state vector.</value>
        public IStateVector StateVector { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationEventArgs" /> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="stateVector">The state vector.</param>
        public StateEventArgs(ISimulationTime time, IStateVector stateVector)
            : base(time)
        {
            StateVector = stateVector;
        }
    }
}
