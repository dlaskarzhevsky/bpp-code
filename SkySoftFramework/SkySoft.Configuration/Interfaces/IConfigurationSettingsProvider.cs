using SkySoft.Core;

namespace SkySoft.Configuration
{
    /// <summary>
    /// Defines configuration settings provider public members
    /// </summary>
    public interface IConfigurationSettingsProvider : ISettingsProvider
    {
        #region Properties
        /// <summary>
        /// Gets flag indicating whether configuration settings should be appended under configuration settings provider
        /// </summary>
        bool AppendSettings
        {
            get;
        }

        /// <summary>
        /// Gets or sets flag indicating whether external configuration section was loaded
        /// </summary>
        bool ExternalConfigurationSectionWasLoaded
        {
            get;
            set;
        }

        /// <summary>
        /// Gets provider name
        /// </summary>
        string ProviderName
        {
            get;
        }

        /// <summary>
        /// Gets flag which indicates whether configuration settings should be loaded from externnal configuration file
        /// </summary>
        bool UseExternalConfigurationFile
        {
            get;
        }
        #endregion
    }
}
