using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.Compiler
{
    /// <summary>
    /// A compiled matrix
    /// </summary>
    /// <typeparam name="TMatrix"></typeparam>
    public sealed class CompiledMatrix<TMatrix>
        where TMatrix : IMatrix
    {
        /// <summary>
        /// The matrix to work with
        /// </summary>
        private readonly TMatrix _prototype;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompiledMatrix{TMatrix}" /> class.
        /// </summary>
        /// <param name="prototype">The prototype.</param>
        public CompiledMatrix(TMatrix prototype)
        {
            _prototype = prototype;
        }

        /// <summary>
        /// Compiles the matrix
        /// </summary>
        public void Compile()
        {
            // var expression = GetValueExpression(0, 1);
        }

        /// <summary>
        /// Gets the value expression.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>Expression.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">row;Row must be in range 0..(Prototype.Rows-1)</exception>
        private Expression GetValueExpression(int row, int column, ParameterExpression matrix)
        {
            /*
            if (row < 0 || row >= _prototype.Rows) throw new ArgumentOutOfRangeException("row", "Row must be in range 0..(Prototype.Rows-1)");
            if (column < 0 || column >= _prototype.Columns) throw new ArgumentOutOfRangeException("column", "Row must be in range 0..(Prototype.Columns-1)");
            if (matrix == null) throw new ArgumentNullException("matrix", "Parameter expression must not be null");

            var valueProvider = _prototype.GetValue(column, row);

            // Get the expression
            var expressionProvider = valueProvider as IExpressionProvider;
            if (expressionProvider != null)
            {
                return expressionProvider.GetExpression(matrix);
            }

            // Create a variable expression
            return Expression.Property(matrix, "Item", row, column);
            */
            return null;
        }
    }
}
