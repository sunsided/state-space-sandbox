using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// State vector implementation
    /// </summary>
    internal sealed class StateVector : AbstractVector, IStateVector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlVector" /> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public StateVector(int length)
            : base(length)
        {
        }

        /// <summary>
        /// Adds another control vector
        /// </summary>
        /// <param name="other">The other vector</param>
        public IStateVector Add(IStateVector other)
        {
            IVector result = new StateVector(Length);
            Add(other, ref result);
            return (IStateVector)result;
        }

        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="other">The vector to add</param>
        public void AddInPlace(IStateVector other)
        {
            base.AddInPlace(other);
        }

        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="other">The vector to add</param>
        /// <param name="output">The output.</param>
        public void Add(IStateVector other, ref IStateVector output)
        {
            IVector result = output;
            Add(other, ref result);
        }
    }
}
