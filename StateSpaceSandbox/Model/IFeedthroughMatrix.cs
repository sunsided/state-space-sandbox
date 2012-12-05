namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a feedthrough/feedforward matrix ("D")
    /// </summary>
    public interface IFeedthroughMatrix : ITransformationMatrix<IControlVector, IOutputVector>
    {
    }
}
