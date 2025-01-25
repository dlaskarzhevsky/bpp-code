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
            bool? applicationInitialized = operatingSystem.GetValueFomCache<bool?>(TargetStateName);
            if (applicationInitialized == null || applicationInitialized == false)
            {
                IDataContainer dataContainer = operatingSystem.GetNewDataContainer();
                ConfigureRequestToInitializeApplication(dataContainer);
                dataContainer = await operatingSystem.InitializeApplication(dataContainer);
                LogMessages.Execute(dataContainer, operatingSystem);
                if (ApplicationInitialized(dataContainer))
                {
                    operatingSystem.CacheValue<bool>(TargetStateName, true);
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Configures request to initialize application
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        protected virtual void ConfigureRequestToInitializeApplication(IDataContainer dataContainer)
        {
            dataContainer.DomainName = DomainName;
            dataContainer.UseCaseName = UseCaseName;
            dataContainer.ApplicationLayerName = ApplicationLayerName;
            dataContainer.TransitionName = TransitionName;
        }

        /// <summary>
        /// Gets flag indicating whether application initialized
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>True if application initialized, otherwise False</returns>
        protected virtual bool ApplicationInitialized(IDataContainer dataContainer)
        {
            bool applicationInitialized = dataContainer.Exception == null && string.IsNullOrEmpty(dataContainer.Message) || !string.IsNullOrEmpty(dataContainer.Message) && dataContainer.MessageType != MessageType.Critical && dataContainer.MessageType != MessageType.Error;
            return applicationInitialized;
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
    }
}
