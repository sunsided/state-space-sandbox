using System.Linq.Expressions;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Provider for <see cref="Expression"/> instances
    /// </summary>
    public interface IExpressionProvider
    {
        /// <summary>
        /// Gets an expression that provides this item's value
        /// </summary>
        /// <returns>The expression</returns>
        Expression GetExpression(ParameterExpression element);
    }
}
