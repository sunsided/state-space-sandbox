namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes an input matrix ("B")
    /// </summary>
    public interface IInputMatrix : ITransformationMatrix<IControlVector, IStateVector>
    {
    }
}
