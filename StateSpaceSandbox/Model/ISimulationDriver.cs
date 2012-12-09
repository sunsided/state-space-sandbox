using System;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Drives the simulation
    /// </summary>
    public interface ISimulationDriver
    {
        /// <summary>
        /// Starts the simulation
        /// </summary>
        void Start();

        /// <summary>
        /// Pauses the simulation
        /// </summary>
        void Pause();

        /// <summary>
        /// Occurs when control vector has changed.
        /// </summary>
        event EventHandler<EventArgs> ControlChanged;

        /// <summary>
        /// Occurs when the state vector has changed.
        /// </summary>
        event EventHandler<EventArgs> StateChanged;
        
        /// <summary>
        /// Occurs when the state vector has changed.
        /// </summary>
        event EventHandler<EventArgs> OutputChanged;
    }
}
