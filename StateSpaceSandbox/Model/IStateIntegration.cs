namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Describes a state integration
    /// </summary>
    public interface IStateIntegration : IVectorTransformation<IStateVector, IStateVector>
    {
    }
}
