using System;

namespace SkySoft.Core
{
    /// <summary>
    /// Contains System.TimeSpan extention methods
    /// </summary>
    public static class TimeSpanExtensionMethods
    {
        #region Methods
        /// <summary>
        /// Rounds time span to the specified precision
        /// </summary>
        /// <param name="roundWhat">Time span for rounding</param>
        /// <param name="roundTo">Rounding precision, for example pass TimeSpan.FromMinutes(1) for rounding to one minute or TimeSpan.FromSeconds(1) for rounding to one second</param>
        /// <returns>Rounded time span</returns>
        public static TimeSpan Round(this TimeSpan roundWhat, TimeSpan roundTo)
        {
            if (roundTo == TimeSpan.Zero)
            {
                return roundWhat;
            }

            long roundToTicks = roundTo.Ticks;
            long roundedTicks = roundWhat.Ticks + roundToTicks / 2;
            roundedTicks -= roundedTicks % roundToTicks;

            return TimeSpan.FromTicks(roundedTicks);
        }
        #endregion
    }
}
