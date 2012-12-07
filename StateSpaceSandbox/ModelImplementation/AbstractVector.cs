using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using StateSpaceSandbox.Model;

namespace StateSpaceSandbox.ModelImplementation
{
    /// <summary>
    /// Basic vector
    /// </summary>
    internal abstract class AbstractVector : IVector
    {
        /// <summary>
        /// The data
        /// </summary>
        private readonly double[] _data;

        /// <summary>
        /// The method used to add two vectors
        /// </summary>
        private static Action<double[], IVector, IVector> _additionExpression;

        /// <summary>
        /// The method used to add two vectors in-place
        /// </summary>
        private static Action<double[], IVector> _additionInPlaceExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractVector" /> class.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">length;Vector length must be greater or equal to 1</exception>
        public AbstractVector(int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException("length", "Vector length must be greater or equal to 1");
            _data = new double[length];

            _additionExpression = BuildAdditionExpression(length);
            _additionInPlaceExpression = BuildInPlaceAdditionExpression(length);
        }

        /// <summary>
        /// The vector's length
        /// </summary>
        /// <value>The length.</value>
        public int Length { [DebuggerStepThrough, Pure] get { return _data.Length; } }

        /// <summary>
        /// Adds the specified vector.
        /// </summary>
        /// <param name="other">The vector to add.</param>
        /// <returns>IVector.</returns>
        /// <exception cref="System.ArgumentNullException">The other vector must not be null</exception>
        /// <exception cref="System.ArgumentException">The other vector must be of the same length as this instance</exception>
        protected void AddInPlace(IVector other)
        {
            if (other == null) throw new ArgumentNullException("other", "The other vector must not be null");
            if (other.Length != Length) throw new ArgumentException("The other vector must be of the same length as this instance", "other");

            _additionInPlaceExpression(_data, other);
        }

        /// <summary>
        /// Adds the specified vector.
        /// </summary>
        /// <param name="other">The vector to add.</param>
        /// <param name="result">The result.</param>
        /// <returns>IVector.</returns>
        /// <exception cref="System.ArgumentNullException">The other vector must not be null</exception>
        /// <exception cref="System.ArgumentException">The other vector must be of the same length as this instance</exception>
        protected void Add(IVector other, ref IVector result)
        {
            if (other == null) throw new ArgumentNullException("other", "The other vector must not be null");
            if (result == null) throw new ArgumentNullException("result", "The result vector must not be null");
            if (other.Length != Length) throw new ArgumentException("The other vector must be of the same length as this instance", "other");
            if (result.Length != Length) throw new ArgumentException("The result vector must be of the same length as this instance", "other");

            _additionExpression(_data, other, result);
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Double" /> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="IndexOutOfRangeException">The specified index was smaller than zero or greater or equal to <see cref="Length"/></exception>
        public double this[int index]
        {
            [DebuggerStepThrough, Pure] get { return _data[index]; }

            [DebuggerStepThrough] set { _data[index] = value; }
        }

        /// <summary>
        /// Builds the addition expression.
        /// </summary>
        /// <typeparam name="TVector">The type of the T vector.</typeparam>
        /// <param name="length">The length.</param>
        /// <returns>The compiled addition lambda.</returns>
        private static Action<double[], IVector, IVector> BuildAdditionExpression(int length)
        {
            var leftArray = Expression.Parameter(typeof(double[]), "left");
            var rightArray = Expression.Parameter(typeof (IVector), "right");
            var resultArray = Expression.Parameter(typeof (IVector), "result");

            // Prepare the list of expressions for the calculation
            var expressions = new List<Expression>();

            // create the addition statements
            for (int i = 0; i < length; ++i)
            {
                var index = Expression.Constant(i, typeof (int));
                var accessLeft = Expression.ArrayAccess(leftArray, index);
                var accessRight = Expression.Property(rightArray, "Item", index);
                var accessResult = Expression.Property(resultArray, "Item", index);
                
                var addition = Expression.Add(accessLeft, accessRight);
                var assignment = Expression.Assign(accessResult, addition);

                expressions.Add(assignment);
            }

            // return the newly created vector
            expressions.Add(resultArray);

            // combine statements to expression block, then create and compile lambda
            var additionBlock = Expression.Block(expressions);
            var lambda = Expression.Lambda<Action<double[], IVector, IVector>>(additionBlock, "addition", new[] { leftArray, rightArray, resultArray });
            return lambda.Compile();
        }

        /// <summary>
        /// Builds the addition expression.
        /// </summary>
        /// <typeparam name="TVector">The type of the T vector.</typeparam>
        /// <param name="length">The length.</param>
        /// <returns>The compiled addition lambda.</returns>
        private static Action<double[], IVector> BuildInPlaceAdditionExpression(int length)
        {
            var leftArray = Expression.Parameter(typeof(double[]), "left");
            var rightArray = Expression.Parameter(typeof(IVector), "right");

            // Prepare the list of expressions for the calculation
            var expressions = new List<Expression>();

            // create the addition statements
            for (int i = 0; i < length; ++i)
            {
                var index = Expression.Constant(i, typeof(int));
                var accessLeft = Expression.ArrayAccess(leftArray, index);
                var accessRight = Expression.Property(rightArray, "Item", index);

                var assignment = Expression.AddAssign(accessLeft, accessRight);

                expressions.Add(assignment);
            }

            // combine statements to expression block, then create and compile lambda
            var additionBlock = Expression.Block(expressions);
            var lambda = Expression.Lambda<Action<double[], IVector>>(additionBlock, "additionInPlace", new[] { leftArray, rightArray });
            return lambda.Compile();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return String.Join("; ", _data);
        }
    }
}

