using System;
using System.IO;

using Microsoft.Extensions.Configuration;

using SkySoft.Core;

namespace SkySoft.Configuration
{
    /// <summary>
    /// Provides configuration loader functionality
    /// </summary>
    internal class ConfigurationLoader : DataProcessingLogic
    {
        #region Static Public Method
        /// <summary>
        /// Loads application configuration from specified path
        /// </summary>
        /// <param name="pathToRootFolder">Path to root folder (required)</param>
        /// <param name="configurationFileName">Configuration file name (required)</param>
        public static IConfiguration LoadApplicationConfiguration(string pathToRootFolder, string configurationFileName)
        {
            ConfigurationLoader configurationLoader = new ConfigurationLoader(pathToRootFolder, configurationFileName);
            configurationLoader.ManageDataProcessing();

            IConfiguration applicationConfiguration = ApplicationConfiguration;
            ApplicationConfiguration = default!;
            return applicationConfiguration;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pathToRootFolder">Path to root folder (required)</param>
        /// <param name="configurationFileName">Configuration file name (required)</param>
        public ConfigurationLoader(string pathToRootFolder, string configurationFileName)
        {
            PathToRootFolder = pathToRootFolder;
            ConfigurationFileName = configurationFileName;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            LoadConfiguration();
        }

        /// <summary>
        /// Initializes component
        /// </summary>
        protected override void InitializeComponent()
        {
            if (string.IsNullOrEmpty(ConfigurationFileName))
            {
                ComponentInitialized = false;
            }

            if (string.IsNullOrEmpty(PathToRootFolder))
            {
                ComponentInitialized = false;
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        protected override void ReleaseResources()
        {
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        static IConfiguration ApplicationConfiguration
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets comfiguration file name
        /// </summary>
        string ConfigurationFileName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets path to root folder
        /// </summary>
        string PathToRootFolder
        {
            get; set;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Loads configuration
        /// </summary>
        void LoadConfiguration()
        {
            string fullPathToConfigurationFile = Path.Combine(PathToRootFolder, ConfigurationFileName);
            if (System.IO.File.Exists(fullPathToConfigurationFile))
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(PathToRootFolder, ConfigurationFileName));
                ApplicationConfiguration = builder.Build(); 
            }
            /*
                        var appSettingsSoftwareLayerNames1 = configuration["SoftwareSection:SoftwareSettings:AppSettingsSoftwareLayerNames"];
                        var softwareSection = configuration.GetSection("SoftwareSection");
                        var appSettingsSoftwareLayerNames2 = softwareSection["SoftwareSettings:AppSettingsSoftwareLayerNames"];
                        var softwareSettings = softwareSection.GetSection("SoftwareSettings");
                        var appSettingsSoftwareLayerNames3 = softwareSettings["AppSettingsSoftwareLayerNames"];
                        SoftwareSection softwareSectionInstance = new SoftwareSection();
                        configuration.Bind("SoftwareSection", softwareSectionInstance);
            */
        }
        #endregion
    }
}
