using System;

namespace StateSpaceSandbox.Random
{
    /// <summary>
    /// Xorshift-128 Random Number Generator
    /// </summary>
    public sealed class XorShift160
    {
        private ulong _x;
        private ulong _y;
        private ulong _z;
        private ulong _w;
        private ulong _v;
        private ulong _d;

        /// <summary>
        /// Initializes a new instance of the <see cref="XorShift160" /> class.
        /// </summary>
        public XorShift160()
            : this(123456789 ^ Environment.TickCount, 362436069 ^ Environment.TickCount, 521288629 ^ Environment.TickCount, 88675123 ^ Environment.TickCount, 5783321 ^ Environment.TickCount, 6615241 ^ Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XorShift160" /> class.
        /// </summary>
        /// <param name="seedX">The seed X.</param>
        /// <param name="seedY">The seed Y.</param>
        /// <param name="seedZ">The seed Z.</param>
        /// <param name="seedW">The seed W.</param>
        public XorShift160(long seedX, long seedY, long seedZ, long seedW, long seedV, long seedD)
        {
            _x = (uint)seedX;
            _y = (uint)seedY;
            _z = (uint)seedZ;
            _w = (uint)seedW;
            _v = (uint)seedV;
            _d = (uint)seedD;
        }

        /// <summary>
        /// Gets the next integer value
        /// </summary>
        /// <returns>The random number</returns>
        public long NextLong()
        {
            // http://www.jstatsoft.org/v08/i14/paper

            ulong t = _x ^ (_x >> 2);
            _x = _y;
            _y = _z;
            _z = _w;
            _w = _v;
            _v = (_v ^ (_v << 2)) ^ (t ^ (t << 1));
            _d += 362437 + _v;

            return (long)_d;
        }

        /// <summary>
        /// Gets the next integer value
        /// </summary>
        /// <returns>The random number</returns>
        public int Next()
        {
            return (int)NextLong();
        }

        /// <summary>
        /// Gets the next double value
        /// </summary>
        /// <returns>The random number</returns>
        public double NextDouble()
        {
            ulong rnd = (ulong)NextLong();
            double scaling = 1.0D/UInt64.MaxValue;
            double value = 2.0D * (rnd * scaling - 0.5D);
            return value;
        }
    }
}
