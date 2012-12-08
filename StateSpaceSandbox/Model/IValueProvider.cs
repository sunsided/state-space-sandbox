namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Provider for values
    /// </summary>
    public interface IValueProvider
    {
        /// <summary>
        /// Gets the value
        /// </summary>
        double GetValue(ISimulationTime time);
    }
}
