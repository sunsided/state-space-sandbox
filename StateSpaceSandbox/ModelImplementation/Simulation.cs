using System;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// The simulation
    /// </summary>
    public sealed class Simulation : ISimulation
    {
        /// <summary>
        /// The simulation time
        /// </summary>
        private readonly SimulationTime _simulationTime;

        /// <summary>
        /// The simulation time
        /// </summary>
        /// <value>The simulation time.</value>
        public ISimulationTime SimulationTime
        {
            get { return _simulationTime; }
        }

        /// <summary>
        /// Gets the state matrix A
        /// </summary>
        /// <value>The state matrix.</value>
        public IStateMatrix StateMatrix { get; private set; }

        /// <summary>
        /// Gets the input matrix B
        /// </summary>
        /// <value>The input matrix.</value>
        public IInputMatrix InputMatrix { get; private set; }

        /// <summary>
        /// Gets the output matrix C
        /// </summary>
        /// <value>The output matrix.</value>
        public IOutputMatrix OutputMatrix { get; private set; }

        /// <summary>
        /// Gets the feedthrough matrix D
        /// </summary>
        /// <value>The feedthrough matrix.</value>
        public IFeedthroughMatrix FeedthroughMatrix { get; private set; }

        /// <summary>
        /// Gets the current state vector x
        /// </summary>
        /// <value>The state vector.</value>
        public IStateVector StateVector { get; private set; }

        /// <summary>
        /// Gets the current state vector x's differential
        /// </summary>
        /// <value>The state vector.</value>
        public IStateVector StateVectorDifferential { get; private set; }

        /// <summary>
        /// Gets the current control vector u
        /// </summary>
        /// <value>The control vector.</value>
        public IControlVector ControlVector { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Simulation" /> class.
        /// </summary>
        /// <param name="stateMatrix">The state matrix.</param>
        /// <param name="inputMatrix">The input matrix.</param>
        /// <param name="outputMatrix">The output matrix.</param>
        /// <param name="feedthroughMatrix">The feedthrough matrix.</param>
        /// <param name="initialStateVector">The initial state vector.</param>
        /// <param name="controlVector">The control vector.</param>
        public Simulation(IStateMatrix stateMatrix, IInputMatrix inputMatrix, IOutputMatrix outputMatrix, IFeedthroughMatrix feedthroughMatrix, IStateVector initialStateVector, IControlVector controlVector)
        {
            StateMatrix = stateMatrix;
            InputMatrix = inputMatrix;
            OutputMatrix = outputMatrix;
            FeedthroughMatrix = feedthroughMatrix;
            StateVector = initialStateVector;
            ControlVector = controlVector;
            _simulationTime = new SimulationTime();
        }

        /// <summary>
        /// Advances the simulation by the tiem given in <paramref name="step" />
        /// </summary>
        /// <param name="step">The time step</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void AdvanceSimulation(TimeSpan step)
        {
            _simulationTime.Add(step);

            // Fetch new input values
            ControlVector.Update(_simulationTime);

            // Update matrices
            InputMatrix.Update(_simulationTime);
            StateMatrix.Update(_simulationTime);
            OutputMatrix.Update(_simulationTime);
            FeedthroughMatrix.Update(_simulationTime);

            // Calculate
            IStateVector ax = new StateVector(StateVector.Length);
            StateMatrix.Transform(StateVector, ref ax);

            IStateVector bu = new StateVector(StateVector.Length);
            StateMatrix.Transform(StateVector, ref ax);
        }
    }
}
