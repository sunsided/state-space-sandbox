namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a transformation matrix
    /// </summary>
    public interface ITransformationMatrix<in TInput, TOutput> : IMatrix, IVectorTransformation<TInput, TOutput> 
        where TInput : IVector
        where TOutput : IVector
    {
    }
}
