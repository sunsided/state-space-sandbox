namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a state vector ("x")
    /// </summary>
    public interface IStateVector : IDynamicVector
    {
        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="other">The vector to add</param>
        /// <returns>IStateVector.</returns>
        IStateVector Add(IStateVector other);

        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="other">The vector to add</param>
        void AddInPlace(IStateVector other);

        /// <summary>
        /// Adds a state vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="other">The vector to add</param>
        /// <param name="output">The output.</param>
        void Add(IStateVector other, ref IStateVector output);
    }
}
