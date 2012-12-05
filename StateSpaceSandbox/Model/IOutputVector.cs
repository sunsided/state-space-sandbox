namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes the output vector
    /// </summary>
    public interface IOutputVector : IVector
    {
        /// <summary>
        /// Adds a control vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="other">The vector to add</param>
        IOutputVector Add(IOutputVector other);

        /// <summary>
        /// Adds a control vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="other">The vector to add</param>
        void AddInPlace(IOutputVector other);

        /// <summary>
        /// Adds a control vector to this instance and returns the summed vector
        /// </summary>
        /// <param name="other">The vector to add</param>
        /// <param name="output">The output.</param>
        void Add(IOutputVector other, ref IOutputVector output);
    }
}
