namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a control (input) vector ("u")
    /// </summary>
    public interface IControlVector : IReadableVector, ISimulationUpdate
    {
    }
}
