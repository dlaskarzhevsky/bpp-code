namespace SkySoft.IBPPApplication
{
    /// <summary>
    /// Defines application initializer functionality
    /// </summary>
    public interface IApplicationInitializer
    {
        #region Methods
        /// <summary>
        /// Initializes application
        /// </summary>
        /// <param name="operatingSystem">Operating system</param>
        /// <returns>True if application was initialized, otherwise false</returns>
        Task<bool> InitializeApplication(IOS operatingSystem);
        #endregion
    }
}
