using System.Diagnostics;

namespace Oed.ExtensionsLibrary.LanguageExtensions
{
    public static class NumericExtensions
    {
        /// <summary>
        /// Flip negative to positive or positive to negative
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static int Invert(this int value) 
            => value * (-1);

        /// <summary>
        /// Determine if sender is even
        /// </summary>
        /// <param name="value">Int to work against</param>
        /// <returns>True if even, false if odd</returns>
        [DebuggerStepThrough]
        public static bool IsEven(this int value) 
            => value % 2 == 0;

        /// <summary>
        /// Determine if sender is odd
        /// </summary>
        /// <param name="value">Int to work against</param>
        /// <returns>True if odd, false if even</returns>
        [DebuggerStepThrough]
        public static bool IsOdd(this int value) 
            => !IsEven(value);

        /// <summary>
        /// Get parts of a double
        /// </summary>
        /// <param name="value">value to get parts</param>
        /// <returns>tuple, major and fraction from sender</returns>
        /// <code>
        /// double value = 11.33;
        /// var (major, fraction) = value.GetParts();
        /// Assert.AreEqual(major, 11);
        /// Assert.AreEqual(fraction, (decimal).33);
        /// </code>
        [DebuggerStepThrough]
        public static (int major, decimal fraction) GetParts(this double value)
        {
            decimal fraction = (decimal)value;
            int majorPart = (int)fraction;
            decimal decimalPart = fraction % 1.0m;

            return (majorPart, decimalPart);
        }
        [DebuggerStepThrough]
        public static int GetMajor(this decimal value)
            => (int)value;
    }
}