namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a vector transformation
    /// </summary>
    public interface IVectorTransformation<in TInput, TOutput>
        where TInput : IVector
        where TOutput : IVector
    {
        /// <summary>
        /// Transforms the given vector
        /// </summary>
        /// <param name="vector">The vector to transform</param>
        /// <returns>The transformed vector</returns>
        TOutput Transform(TInput vector);

        /// <summary>
        /// Transforms the given vector
        /// </summary>
        /// <param name="vector">The vector to transform</param>
        /// <param name="output">The output.</param>
        /// <returns>The transformed vector</returns>
        void Transform(TInput vector, ref TOutput output);
    }
}
