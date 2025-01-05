namespace SkySoft.Configuration
{
    /// <summary>
    /// Provides application configuration section functionality
    /// </summary>
    public class ApplicationConfigurationSection : IApplicationConfigurationSection
    {
        #region Properties
        /// <summary>
        /// Gets or sets configuration file name
        /// IApplicationConfigurationSection interface implementation
        /// </summary>
        public string? ConfigSource
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets registries
        /// </summary>
        public Registries? Registries
        {
            get; set;
        }
        #endregion
    }
}
