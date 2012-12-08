using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// The simulation time provider
    /// </summary>
    public interface ISimulationTime
    {
        /// <summary>
        /// Gets the simulation step
        /// </summary>
        ulong SimulationStep { get; }

        /// <summary>
        /// Gets the current simulation time
        /// </summary>
        TimeSpan Time { get; }

        /// <summary>
        /// Gets the time differential (distance to the last time slot) in unit seconds
        /// </summary>
        double TimeDifferential { get; }
    }
}
