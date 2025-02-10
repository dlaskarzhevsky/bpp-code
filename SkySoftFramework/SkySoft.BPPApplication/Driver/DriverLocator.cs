using SkySoft.IBPPApplication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides driver locator functionality
    /// </summary>
    class DriverLocator
    {
        #region Public Methods
        /// <summary>
        /// Finds driver
        /// </summary>
        /// <param name="drivers">Set of drivers</param>
        /// <param name="controllerType">Controller type</param>
        /// <returns>Found driver or null</returns>
        public static IDriver? FindDriver(IEnumerable<IDriver> drivers, string? controllerType)
        {
            if (string.IsNullOrEmpty(controllerType))
            {
                return null;
            }

            IDriver? foundDriver = null;
            foreach (IDriver driver in drivers)
            {
                if (driver.ControllerType == controllerType)
                {
                    foundDriver = driver;
                    break;
                }
            }

            return foundDriver;
        }
        #endregion
    }
}
