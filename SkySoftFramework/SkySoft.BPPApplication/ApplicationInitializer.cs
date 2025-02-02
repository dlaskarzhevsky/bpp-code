using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides API host initializer functionality
    /// </summary>
    public class ApplicationInitializer : IApplicationInitializer
    {
        #region Public Methods
        /// <summary>
        /// Initializes application
        /// IApplicationInitializer interface implementation
        /// </summary>
        /// <param name="operatingSystem">Operating system</param>
        /// <returns>True if application was initialized, otherwise false</returns>
        public virtual async Task<bool> InitializeApplication(IOS operatingSystem)
        {
            OperatingSystem = operatingSystem;
            ConfigureRequestForApplicationInitialization();
            PreconfigureOperatingSystem();
            await SendRequestForApplicationInitializationToOperatingSystem();
            FinalizeOperatingSystemConfiguration();
            LogMessages.Execute(DataContainer, operatingSystem);
            if (OperatingSystemInitializedApplication)
            {
                FinalizeApplicationInitializer();
            }

            ReleaseResources();
            return ApplicationInInitialState;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets flag indicating whether application in initial state
        /// </summary>
        public bool ApplicationInInitialState
        {
            get
            {
                return ApplicationState == TargetStateName;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Finalizes operating system configuration
        /// </summary>
        protected virtual void FinalizeOperatingSystemConfiguration()
        {
        }

        /// <summary>
        /// Finalizes application initializer
        /// </summary>
        protected virtual void FinalizeApplicationInitializer()
        {
        }

        /// <summary>
        /// Preconfigures operating system
        /// </summary>
        protected virtual void PreconfigureOperatingSystem()
        {
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets or sets application layer name
        /// </summary>
        protected string ApplicationLayerName
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets application layer name
        /// </summary>
        protected string ApplicationLayerFullName
        {
            get
            {
                return $"{DomainName}_{UseCaseName}_{ApplicationLayerName}";
            }
        }

        /// <summary>
        /// Gets or sets application state
        /// </summary>
        protected string? ApplicationState
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets data container
        /// </summary>
        protected IDataContainer DataContainer
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets domain name
        /// </summary>
        protected string DomainName
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets operating system
        /// </summary>
        protected IOS OperatingSystem
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets flag indicating whether operating system initialized application
        /// </summary>
        protected bool OperatingSystemInitializedApplication
        {
            get
            {
                return DataContainer.StateName == TargetStateName;
            }
        }

        /// <summary>
        /// Gets or sets target state name
        /// </summary>
        protected string TargetStateName
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets transition name
        /// </summary>
        protected string TransitionName
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets application layer name
        /// </summary>
        protected string UseCaseName
        {
            get; set;
        } = default!;
        #endregion

        #region Private Methods
        /// <summary>
        /// Configures request for application initialization
        /// </summary>
        void ConfigureRequestForApplicationInitialization()
        {
            DataContainer = OperatingSystem.GetNewDataContainer();
            DataContainer.DomainName = DomainName;
            DataContainer.UseCaseName = UseCaseName;
            DataContainer.ApplicationLayerName = ApplicationLayerName;
            DataContainer.TransitionName = TransitionName;
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        void ReleaseResources()
        {
            DataContainer = default!;
            OperatingSystem = default!;
        }

        /// <summary>
        /// Sends request for application initialization to operating system
        /// </summary>
        async Task SendRequestForApplicationInitializationToOperatingSystem()
        {
            DataContainer = await OperatingSystem.RedirectRequestToRequestHandler(DataContainer);
        }
        #endregion
    }
}
