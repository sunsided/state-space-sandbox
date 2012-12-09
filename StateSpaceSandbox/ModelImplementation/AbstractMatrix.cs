using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using StateSpaceSandbox.Compiler;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// Basic matrix
    /// </summary>
    [DebuggerDisplay("{Rows} x {Columns}")]
    internal abstract class AbstractMatrix : IMatrix, ISimulationUpdate
    {
        /// <summary>
        /// The data
        /// </summary>
        private readonly IValueProvider[,] _data;

        /*
        /// <summary>
        /// The method used to add two vectors
        /// </summary>
        private readonly Action<IMatrix, IVector, IVector> _transformationExpression;
        */

        /// <summary>
        /// The width
        /// </summary>
        private readonly int _columns;
        
        /// <summary>
        /// The height
        /// </summary>
        private readonly int _rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractVector" /> class.
        /// </summary>
        /// <param name="columns">The width.</param>
        /// <param name="rows">The height.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">length;Vector length must be greater or equal to 1</exception>
        public AbstractMatrix(int rows, int columns)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException("columns", "Matrix width must be greater or equal to 1");
            if (rows <= 0) throw new ArgumentOutOfRangeException("rows", "Matrix height must be greater or equal to 1");
            _columns = columns;
            _rows = rows;
            _data = new IValueProvider[rows, columns];
         
            //_transformationExpression = BuildTransformationExpression(columns, rows);
        }

        /// <summary>
        /// Gets the number of columns
        /// </summary>
        /// <value>The columns.</value>
        public int Columns { [DebuggerStepThrough, Pure] get { return _columns; } }

        /// <summary>
        /// Gets the number of rows
        /// </summary>
        /// <value>The rows.</value>
        public int Rows { [DebuggerStepThrough, Pure] get { return _rows; } }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>IValueProvider.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public double GetValue(int row, int column)
        {
            var value = _data[row, column] ?? (_data[row, column] = new ConstantValue(0));
            return value.Value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>IValueProvider.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IValueProvider GetValueProvider(int row, int column)
        {
            return _data[row, column];
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetValue(int row, int column, IValueProvider value)
        {
            _data[row, column] = value;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetValue(int row, int column, double value)
        {
            _data[row, column] = new ConstantValue(value);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="valueFunction">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetValue(int row, int column, Func<ISimulationTime, double> valueFunction)
        {
            _data[row, column] = new RuntimeValue(valueFunction);
        }

        /// <summary>
        /// Updates the element
        /// </summary>
        /// <param name="time">The current time</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(ISimulationTime time)
        {
            for (int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    var updatable = _data[r, c] as ISimulationUpdate;
                    if (updatable == null) continue;
                    updatable.Update(time);
                }
            }
        }

        /// <summary>
        /// Transforms the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="resultingVector">The resulting vector.</param>
        /// <exception cref="System.ArgumentException">The input vector must have the same length as this matrix has columns;vector</exception>
        public void Transform(IReadableVector vector, ref IWritableVector resultingVector)
        {
            if (vector.Length != _columns) throw new ArgumentException("The input vector must have the same length as this matrix has columns", "vector");
            if (resultingVector.Length != _rows) throw new ArgumentException("The resulting vector must have the same length as this matrix has rows", "vector");
            
            // _transformationExpression(this, vector, resultingVector);
            for (int r = 0; r < Rows; ++r)
            {
                double sum = 0;
                for (int c = 0; c < Columns; ++c)
                {
                    var m = GetValue(r, c);
                    var v = vector.GetValue(c);
                    sum += m*v;
                }
                resultingVector.SetValue(r, sum);
            }
        }

        /// <summary>
        /// Builds the addition expression.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="rows">The rows.</param>
        /// <returns>The compiled addition lambda.</returns>
        private static Action<IMatrix, IVector, IVector> BuildTransformationExpression(int columns, int rows)
        {
            var leftArray = Expression.Parameter(typeof(IMatrix), "left");
            var rightArray = Expression.Parameter(typeof (IVector), "right");
            var resultArray = Expression.Parameter(typeof(IVector), "result");

            // Prepare the list of expressions for the calculation
            var expressions = new List<Expression>();

            // create the addition statements
            for (int r = 0; r < rows; ++r)
            {
                var row = Expression.Constant(r, typeof(int));

                BinaryExpression runningSum = null;
                for (int c = 0; c < columns; ++c)
                {
                    var column = Expression.Constant(c, typeof(int));

                    var accessLeft = Expression.Property(leftArray, "Item", row, column); // TODO: Optimizations for matrices that are know to be fix - i.e. values given at construction time - hard code values and remove zero value multiplications
                    var accessRight = Expression.Property(rightArray, "Item", column); // TODO: ^-- Optimize() call could re-compute expression tree with actual values
                    var multiplication = Expression.Multiply(accessLeft, accessRight);
                    if (runningSum == null)
                    {
                        runningSum = multiplication;
                    }
                    else
                    {
                        runningSum = Expression.Add(runningSum, multiplication);
                    }
                }
                
                Trace.Assert(runningSum != null, "Running sum should be non-null as per now");
                var accessResult = Expression.Property(resultArray, "Item", row);
                var assignment = Expression.Assign(accessResult, runningSum);

                expressions.Add(assignment);
            }

            // return the newly created vector
            expressions.Add(resultArray);

            // combine statements to expression block, then create and compile lambda
            var additionBlock = Expression.Block(expressions);
            var lambda = Expression.Lambda<Action<IMatrix, IVector, IVector>>(additionBlock, "addition", new[] { leftArray, rightArray, resultArray });
            return lambda.Compile();
        }
    }
}

