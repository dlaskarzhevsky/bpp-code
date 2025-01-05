using System;
using System.IO;

using Microsoft.Extensions.Configuration;

using SkySoft.Core;

namespace SkySoft.Configuration
{
    /// <summary>
    /// Provides cofiguration system functionality
    /// </summary>
    public class ConfigurationSystem
    {
        #region Data Fields
        /// <summary>
        /// Holds application configuration
        /// </summary>
        static IConfiguration _applicationConfiguration = default!;
        #endregion

        #region Methods
        /// <summary>
        /// Gets entry from application settings
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <returns>Application setting</returns>
        public static string? GetEntryFromAppSettings(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _applicationConfiguration["AppSettings:" + name];
        }

        /// <summary>
        /// Gets entry from connection strings
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <returns>Connection string</returns>
        public static string? GetEntryFromConnectionStrings(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _applicationConfiguration["ConnectionStrings:" + name];
        }

        /// <summary>
        /// Loads application configuration from specified path
        /// </summary>
        /// <param name="path">Path to configuration file (required)</param>
        public static void LoadApplicationConfiguration(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            string? pathToRootFolder = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(pathToRootFolder))
            {
                throw new ArgumentOutOfRangeException(nameof(path));
            }

            ObjectCreator.PathToRootFolder = pathToRootFolder;
            string configurationFileName = Path.GetFileName(path);
            _applicationConfiguration = ConfigurationLoader.LoadApplicationConfiguration(ObjectCreator.PathToRootFolder, configurationFileName);
        }

        /// <summary>
        /// Loads configuration section
        /// </summary>
        /// <param name="sectionName">Section name (required)</param>
        /// <returns>Loaded configuration section</returns>
        public static IApplicationConfigurationSection LoadConfigurationSection(string sectionName)
        {
            return SectionLoader.Load(_applicationConfiguration, sectionName);
        }

        /// <summary>
        /// Loads software layer configuration from specified path
        /// </summary>
        /// <param name="path">Path to configuration file (required)</param>
        /// <returns>Loaded software layer configuration section</returns>
        public static IApplicationConfigurationSection LoadSoftwareLayerConfigurationSection(string path)
        {
            string configurationFileName = Path.GetFileName(path);
            IConfiguration softwareLayerConfiguration = ConfigurationLoader.LoadApplicationConfiguration(ObjectCreator.PathToRootFolder, configurationFileName);
            return SectionLoader.Load(softwareLayerConfiguration, "ApplicationConfigurationSection");
        }
        #endregion
    }
}
