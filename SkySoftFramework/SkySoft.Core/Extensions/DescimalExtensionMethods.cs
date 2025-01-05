using System;

namespace SkySoft.Core
{
    /// <summary>
    /// Contains System.Decimal extention methods
    /// </summary>
    public static class DescimalExtensionMethods
    {
        #region Methods
        /// <summary>
        /// Gets the maximum value for specified precision and scale
        /// </summary>
        /// <param name="value">Decimal value</param>
        /// <param name="precision">Specified precision</param>
        /// <param name="scale">Specified scale</param>
        /// <returns>Maximum value for specified precision and scale</returns>
        public static decimal? GetMaximumValue(this decimal? value, int? precision, int? scale)
        {
            if (precision == null || precision <= 0)
            {
                return null;
            }

            if (scale == null)
            {
                scale = 0;
            }

            string maxValueString = new string('9', precision.Value).Insert(precision.Value - scale.Value, ".");
            return Decimal.Parse(maxValueString);
        }

        /// <summary>
        /// Gets the maximum value for specified precision and scale
        /// </summary>
        /// <param name="value">Decimal value</param>
        /// <param name="precision">Specified precision</param>
        /// <param name="scale">Specified scale</param>
        /// <returns>Maximum value for specified precision and scale</returns>
        public static decimal GetMaximumValue(this decimal value, int precision, int scale)
        {
            string maxValueString = new string('9', precision).Insert(precision - scale, ".");
            return Decimal.Parse(maxValueString);
        }

        /// <summary>
        ///  Determines whether value falls in the specified range
        /// </summary>
        /// <param name="value">Value for verification</param>
        /// <param name="minValue">Minimum boundary</param>
        /// <param name="maxValue">Maximum boundary</param>
        /// <returns>True if value falls in the specified range, otherwise False</returns>
        public static bool InRange(this decimal value, decimal minValue, decimal maxValue)
        {
            return minValue <= value && value <= maxValue;
        }

        /// <summary>
        ///  Determines whether the number of fractional digits correspond to specified scale
        ///  http://stackoverflow.com/questions/763942/calculate-system-decimal-precision-and-scale
        /// </summary>
        /// <param name="value">Value for verification</param>
        /// <param name="scale">Scale for comparison</param>
        /// <returns>True if supplied decimal value is within range. Otherwise False.</returns>
        public static bool IsValidScale(this decimal value, int? scale)
        {
            if (scale == null)
            {
                return true;
            }

            uint[] bits = (uint[])(object)decimal.GetBits(value);
            uint valueScale = (bits[3] >> 16) & 31;
            return valueScale <= scale;
        }
        #endregion
    }
}
