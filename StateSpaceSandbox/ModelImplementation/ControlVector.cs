using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// Control vector implementation
    /// </summary>
    internal sealed class ControlVector : AbstractVector, IControlVector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlVector" /> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public ControlVector(int length) : base(length)
        {
        }

        /// <summary>
        /// Adds another control vector
        /// </summary>
        /// <param name="other">The other vector</param>
        public void Add(IControlVector other)
        {
            AddInPlace(other);
        }
    }
}
