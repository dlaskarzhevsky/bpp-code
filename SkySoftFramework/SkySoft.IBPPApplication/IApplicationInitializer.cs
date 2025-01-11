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
        void InitializeApplication(IOS operatingSystem);
        #endregion
    }
}
