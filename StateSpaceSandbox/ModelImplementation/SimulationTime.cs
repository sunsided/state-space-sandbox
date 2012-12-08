using System;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// Simulation time
    /// </summary>
    public class SimulationTime : ISimulationTime
    {
        /// <summary>
        /// The simulation step
        /// </summary>
        private ulong _simulationStep;

        /// <summary>
        /// The time
        /// </summary>
        private  TimeSpan _time;
        
        /// <summary>
        /// The time differential
        /// </summary>
        private double _timeDifferential;

        /// <summary>
        /// Gets the simulation step
        /// </summary>
        /// <value>The simulation step.</value>
        public ulong SimulationStep
        {
            get { return _simulationStep; }
        }

        /// <summary>
        /// Gets the current simulation time
        /// </summary>
        /// <value>The time.</value>
        public TimeSpan Time
        {
            get { return _time; }
        }

        /// <summary>
        /// Gets the time differential (distance to the last time slot) in unit seconds
        /// </summary>
        /// <value>The time differential.</value>
        public double TimeDifferential
        {
            get { return _timeDifferential; }
        }

        /// <summary>
        /// Adds the specified time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <exception cref="System.ArgumentException">Simulation time must be positive</exception>
        public void Add(TimeSpan time)
        {
            double seconds = time.TotalSeconds;
            if (time.TotalSeconds <= 0)  throw new ArgumentOutOfRangeException("time", time, "Simulation time step must be positive");
            
            ++_simulationStep;
            _time += time;
            _timeDifferential = seconds;
        }
    }
}
