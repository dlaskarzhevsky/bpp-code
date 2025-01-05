using System;
using Microsoft.Extensions.Configuration;

using SkySoft.Core;

namespace SkySoft.Configuration
{
    /// <summary>
    /// Provides section loader functionality
    /// </summary>
    public class SectionLoader : DataProcessingLogic
    {
        #region Static Public Method
        /// <summary>
        /// Loads configuration section
        /// </summary>
        /// <param name="applicationConfiguration">Application configuration</param>
        /// <param name="sectionName">Section name</param>
        /// <returns>Loaded configuration section</returns>
        public static IApplicationConfigurationSection Load(IConfiguration applicationConfiguration, string sectionName)
        {
            SectionLoader sectionLoader = new SectionLoader(applicationConfiguration, sectionName);
            sectionLoader.ManageDataProcessing();

            IApplicationConfigurationSection section = Section;
            Section = default!;
            return section;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="applicationConfiguration">Application configuration</param>
        /// <param name="sectionName">Section name</param>
        public SectionLoader(IConfiguration applicationConfiguration, string sectionName)
        {
            ApplicationConfiguration = applicationConfiguration;
            SectionName = sectionName;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            FillSectionByConfigurationData();
            if (SectionFilledSuccessfully)
            {
                return;
            }

            LoadSectionConfigurationFromExternalFile();
            FillSectionByConfigurationData();
        }

        /// <summary>
        /// Initializes component
        /// </summary>
        protected override void InitializeComponent()
        {
            if (ApplicationConfiguration == null)
            {
                ComponentInitialized = false;
            }

            if (string.IsNullOrEmpty(SectionName))
            {
                ComponentInitialized = false;
            }

            CreateSection();
            if (Section == null)
            {
                ComponentInitialized = false;
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        protected override void ReleaseResources()
        {
            ApplicationConfiguration = default!;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets application configuration
        /// </summary>
        IConfiguration ApplicationConfiguration
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets section
        /// </summary>
        static IApplicationConfigurationSection Section
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets flag indicating whether section filled successfully
        /// </summary>
        bool SectionFilledSuccessfully
        {
            get
            {
                return string.IsNullOrEmpty(Section.ConfigSource);
            }
        }

        /// <summary>
        /// Gets or sets section name
        /// </summary>
        string SectionName
        {
            get; set;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates section
        /// </summary>
        void CreateSection()
        {
            string? sectionTypeName = ApplicationConfiguration["configSections:" + SectionName + ":Type"];
            if (string.IsNullOrEmpty(sectionTypeName))
            {
                throw new ApplicationException("Section is not configured: " + SectionName);
            }

            object? applicationConfigurationSection = ObjectCreator.Create(sectionTypeName, false);
            if (applicationConfigurationSection == null)
            {
                throw new NullReferenceException("Section is not configured: " + SectionName);
            }

            Section = (IApplicationConfigurationSection)applicationConfigurationSection;
        }

        /// <summary>
        /// Fill section by configuration data
        /// </summary>
        void FillSectionByConfigurationData()
        {
            ApplicationConfiguration.Bind(SectionName, Section);
        }

        /// <summary>
        /// Loads section configuration from external file
        /// </summary>
        void LoadSectionConfigurationFromExternalFile()
        {
            if (string.IsNullOrEmpty(Section.ConfigSource))
            {
                throw new NullReferenceException(nameof(Section.ConfigSource));
            }

            ApplicationConfiguration = ConfigurationLoader.LoadApplicationConfiguration(ObjectCreator.PathToRootFolder, Section.ConfigSource);
        }
        #endregion
    }
}
