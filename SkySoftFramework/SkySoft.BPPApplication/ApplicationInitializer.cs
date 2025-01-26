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
            GetApplicationStateFromCache();
            if (!ApplicationInItitialState)
            {
                ConfigureRequestForApplicationInitialization();
                await SendRequestForApplicationInitializationToOperatingSystem();
                LogMessages.Execute(DataContainer, operatingSystem);
                if (OperatingSystemInitializedApplication)
                {
                    SetApplicationStateToInitial();
                }
            }

            ReleaseResources();
            return ApplicationInItitialState;
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
        /// Gets or sets domain name
        /// </summary>
        protected string DomainName
        {
            get; set;
        } = default!;

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
        /// Gets application state from cache
        /// </summary>
        void GetApplicationStateFromCache()
        {
            ApplicationState = OperatingSystem.GetValueFomCache<string?>("ApplicationState");
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
            DataContainer = await OperatingSystem.InitializeApplication(DataContainer);
        }

        /// <summary>
        /// Sets application state to initial
        /// </summary>
        void SetApplicationStateToInitial()
        {
            ApplicationState = SkySoft.Contracts.StateTypes.INITIAL;
            OperatingSystem.CacheValue<string>("ApplicationState", ApplicationState);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether application not initialized
        /// </summary>
        bool ApplicationInItitialState
        {
            get
            {
                return ApplicationState == SkySoft.Contracts.StateTypes.INITIAL;
            }
        }

        /// <summary>
        /// Gets or sets application state
        /// </summary>
        string? ApplicationState
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets data container
        /// </summary>
        IDataContainer DataContainer
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets operating system
        /// </summary>
        IOS OperatingSystem
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets flag indicating whether operating system initialized application
        /// </summary>
        bool OperatingSystemInitializedApplication
        {
            get
            {
                return DataContainer.Exception == null && string.IsNullOrEmpty(DataContainer.Message) || !string.IsNullOrEmpty(DataContainer.Message) && DataContainer.MessageType != MessageType.Critical && DataContainer.MessageType != MessageType.Error;
            }
        }
        #endregion
    }
}
