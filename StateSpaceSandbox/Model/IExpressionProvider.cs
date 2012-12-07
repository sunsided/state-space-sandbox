using System.Linq.Expressions;

namespace StateSpaceSandbox.Model
{
    /// <summary>
    /// Provider for <see cref="Expression"/> instances
    /// </summary>
    interface IExpressionProvider
    {
        /// <summary>
        /// Gets an expression that provides this item's value
        /// </summary>
        /// <returns>The expression</returns>
        Expression GetExpression();
    }
}
