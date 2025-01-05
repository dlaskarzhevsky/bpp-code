namespace SkySoft.Configuration
{
    /// <summary>
    /// Defines application configuration section functionality
    /// </summary>
    public interface IApplicationConfigurationSection
    {
        #region Properties
        /// <summary>
        /// Gets or sets configuration file name
        /// </summary>
        string? ConfigSource
        {
            get; set;
        }
        #endregion
    }
}
