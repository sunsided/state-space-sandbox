using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// The simulation
    /// </summary>
    public interface ISimulation
    {
        /// <summary>
        /// The simulation time
        /// </summary>
        ISimulationTime SimulationTime { get; }

        /// <summary>
        /// Advances the simulation by the tiem given in <paramref name="step"/>
        /// </summary>
        /// <param name="step">The time step</param>
        void AdvanceSimulation(TimeSpan step);

        /// <summary>
        /// Gets the state matrix A
        /// </summary>
        IStateMatrix StateMatrix { get; }

        /// <summary>
        /// Gets the input matrix B
        /// </summary>
        IInputMatrix InputMatrix { get; }

        /// <summary>
        /// Gets the output matrix C
        /// </summary>
        IOutputMatrix OutputMatrix { get; }

        /// <summary>
        /// Gets the feedthrough matrix D
        /// </summary>
        IFeedthroughMatrix FeedthroughMatrix { get; }

        /// <summary>
        /// Gets the current state vector x
        /// </summary>
        IStateVector StateVector { get; }

        /// <summary>
        /// Gets the current control vector u
        /// </summary>
        IControlVector ControlVector { get; }
    }
}
