using System;

namespace StateSpaceSandbox.Random
{
    /// <summary>
    /// Xorshift-128 Random Number Generator
    /// </summary>
    public sealed class XorShift160
    {
        private uint _x;
        private uint _y;
        private uint _z; 
        private uint _w;
        private uint _v;

        /// <summary>
        /// Initializes a new instance of the <see cref="XorShift160" /> class.
        /// </summary>
        public XorShift160()
            : this(123456789 ^ Environment.TickCount, 362436069 ^ Environment.TickCount, 521288629 ^ Environment.TickCount, 88675123 ^ Environment.TickCount, Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XorShift160" /> class.
        /// </summary>
        /// <param name="seedX">The seed X.</param>
        /// <param name="seedY">The seed Y.</param>
        /// <param name="seedZ">The seed Z.</param>
        /// <param name="seedW">The seed W.</param>
        public XorShift160(int seedX, int seedY, int seedZ, int seedW, int seedV)
        {
            _x = (uint)seedX;
            _y = (uint)seedY;
            _z = (uint)seedZ;
            _w = (uint)seedW;
            _v = (uint)seedV;
        }

        /// <summary>
        /// Gets the next integer value
        /// </summary>
        /// <returns>The random number</returns>
        public int Next()
        {
            const int a = 7;
            const int b = 13;
            const int c = 6;

            uint t = _x ^ (_x << a);
            _x = _y; 
            _y = _z; 
            _z = _w;
            _w = _v;
            _v = (_v ^ (_v >> c)) ^ (t ^ (t >> b));

            return (int)_v;
        }

        /// <summary>
        /// Gets the next double value
        /// </summary>
        /// <returns>The random number</returns>
        public double NextDouble()
        {
            uint rnd = (uint) Next();
            double scaling = 1.0D/UInt32.MaxValue;
            double value = 2.0D * (rnd * scaling - 0.5D);
            return value;
        }
    }
}
