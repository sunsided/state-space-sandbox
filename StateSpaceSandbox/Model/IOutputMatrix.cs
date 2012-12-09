namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes an output matrix ("C")
    /// </summary>
    public interface IOutputMatrix : ITransformationMatrix<IStateVector, IOutputVector>, ISimulationUpdate
    {
    }
}
