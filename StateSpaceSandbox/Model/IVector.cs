namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a vector
    /// </summary>
    public interface IVector
    {
        /// <summary>
        /// The vector's length
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Provides access to the vector elements
        /// </summary>
        /// <param name="index">The index of the vector element</param>
        /// <returns>The value</returns>
        IValueProvider this[int index] { get; set; }
    }
}
