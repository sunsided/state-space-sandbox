using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using StateSpaceSandbox.Compiler;
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
        private readonly IValueProvider[] _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractVector" /> class.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">length;Vector length must be greater or equal to 1</exception>
        public AbstractVector(int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException("length", "Vector length must be greater or equal to 1");
            _data = new IValueProvider[length];
        }

        /// <summary>
        /// The vector's length
        /// </summary>
        /// <value>The length.</value>
        public int Length { [DebuggerStepThrough, Pure] get { return _data.Length; } }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="simulationTime">The simulation time.</param>
        /// <returns>IValueProvider.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public double GetValue(int index, ISimulationTime simulationTime)
        {
            var value = _data[index] ?? (_data[index] = new ConstantValue(0));
            return value.GetValue(simulationTime);
        }

        /// <summary>
        /// Gets the value provider.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>IValueProvider.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IValueProvider GetValueProvider(int index)
        {
            return _data[index];
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetValue(int index, IValueProvider value)
        {
            _data[index] = value;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetValue(int index, double value)
        {
            _data[index] = new ConstantValue(value);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="valueFunction">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetValue(int index, Func<ISimulationTime, double> valueFunction)
        {
            _data[index] = new RuntimeValue(valueFunction);
        }

        /// <summary>
        /// Adds the specified vector.
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add.</param>
        /// <returns>IVector.</returns>
        /// <exception cref="System.ArgumentNullException">The other vector must not be null</exception>
        /// <exception cref="System.ArgumentException">The other vector must be of the same length as this instance</exception>
        protected void AddInPlace(ISimulationTime simulationTime, IVector other)
        {
            if (other == null) throw new ArgumentNullException("other", "The other vector must not be null");
            if (other.Length != Length) throw new ArgumentException("The other vector must be of the same length as this instance", "other");

            for (int i = 0; i < Length; ++i)
            {
                var left = GetValue(i, simulationTime);
                var right = other.GetValue(i, simulationTime);
                SetValue(i, new VariableValue(left + right));
            }
        }

        /// <summary>
        /// Adds the specified vector.
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <param name="other">The vector to add.</param>
        /// <param name="result">The result.</param>
        /// <returns>IVector.</returns>
        /// <exception cref="System.ArgumentNullException">The other vector must not be null</exception>
        /// <exception cref="System.ArgumentException">The other vector must be of the same length as this instance</exception>
        protected void Add(ISimulationTime simulationTime, IVector other, ref IVector result)
        {
            if (other == null) throw new ArgumentNullException("other", "The other vector must not be null");
            if (result == null) throw new ArgumentNullException("result", "The result vector must not be null");
            if (other.Length != Length) throw new ArgumentException("The other vector must be of the same length as this instance", "other");
            if (result.Length != Length) throw new ArgumentException("The result vector must be of the same length as this instance", "other");

            for (int i = 0; i < Length; ++i)
            {
                var left = GetValue(i, simulationTime);
                var right = other.GetValue(i, simulationTime);
                result.SetValue(i, new VariableValue(left + right));
            }
        }

        /*
        /// <summary>
        /// Builds the addition expression.
        /// </summary>
        /// <typeparam name="TVector">The type of the T vector.</typeparam>
        /// <param name="length">The length.</param>
        /// <returns>The compiled addition lambda.</returns>
        private static Action<IVector, IVector, ISimulationTime, IVector> BuildAdditionExpression(int length)
        {
            var leftArray = Expression.Parameter(typeof(IVector), "left");
            var rightArray = Expression.Parameter(typeof (IVector), "right");
            var resultArray = Expression.Parameter(typeof (IVector), "result");
            var simulationTime = Expression.Parameter(typeof (ISimulationTime), "time");

            // Prepare the list of expressions for the calculation
            var expressions = new List<Expression>();

            // create the addition statements
            for (int i = 0; i < length; ++i)
            {
                var index = Expression.Constant(i, typeof (int));
                Expression.Call()
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
            var lambda = Expression.Lambda<Action<IVector, IVector, ISimulationTime, IVector>>(additionBlock, "addition", new[] { leftArray, rightArray, resultArray });
            return lambda.Compile();
        }

        /// <summary>
        /// Builds the addition expression.
        /// </summary>
        /// <typeparam name="TVector">The type of the T vector.</typeparam>
        /// <param name="length">The length.</param>
        /// <returns>The compiled addition lambda.</returns>
        private static Action<IVector, IVector, ISimulationTime> BuildInPlaceAdditionExpression(int length)
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
            var lambda = Expression.Lambda<Action<IVector, IVector, ISimulationTime>>(additionBlock, "additionInPlace", new[] { leftArray, rightArray });
            return lambda.Compile();
        }
        */
    }
}

