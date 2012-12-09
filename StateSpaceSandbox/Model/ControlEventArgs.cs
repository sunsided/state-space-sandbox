using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Simulation related events
    /// </summary>
    public class ControlEventArgs : SimulationEventArgs
    {
        /// <summary>
        /// Gets the state vector.
        /// </summary>
        /// <value>The state vector.</value>
        public IControlVector ControlVector { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlEventArgs" /> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="controlVector">The control vector.</param>
        public ControlEventArgs(ISimulationTime time, IControlVector controlVector)
            : base(time)
        {
            ControlVector = controlVector;
        }
    }
}
