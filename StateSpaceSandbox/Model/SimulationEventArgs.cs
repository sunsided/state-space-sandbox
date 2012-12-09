using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Simulation related events
    /// </summary>
    public class SimulationEventArgs : EventArgs
    {
        /// <summary>
        /// The simulation time
        /// </summary>
        public ISimulationTime Time { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationEventArgs" /> class.
        /// </summary>
        /// <param name="time">The time.</param>
        public SimulationEventArgs(ISimulationTime time)
        {
            Time = time;
        }
    }
}
