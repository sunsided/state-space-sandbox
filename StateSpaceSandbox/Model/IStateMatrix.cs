namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a state matrix ("A")
    /// </summary>
    public interface IStateMatrix : ITransformationMatrix<IStateVector, IStateVector>, ISimulationUpdate
    {
    }
}
