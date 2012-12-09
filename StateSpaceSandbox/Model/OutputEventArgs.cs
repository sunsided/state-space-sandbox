using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Simulation related events
    /// </summary>
    public class OutputEventArgs : SimulationEventArgs
    {
        /// <summary>
        /// Gets the state vector.
        /// </summary>
        /// <value>The state vector.</value>
        public IOutputVector OutputVector { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputEventArgs" /> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="outputVector">The output vector.</param>
        public OutputEventArgs(ISimulationTime time, IOutputVector outputVector)
            : base(time)
        {
            OutputVector = outputVector;
        }
    }
}
