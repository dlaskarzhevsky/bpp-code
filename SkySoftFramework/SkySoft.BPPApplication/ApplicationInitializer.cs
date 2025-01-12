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
        public virtual bool InitializeApplication(IOS operatingSystem)
        {
            bool? applicationInitialized = operatingSystem.GetValueFomCache<bool?>(StateName);
            if (applicationInitialized == null || applicationInitialized == false)
            {
                IDataContainer dataContainer = operatingSystem.GetNewDataContainer();
                ConfigureRequestToLoadDefaultUseCase(dataContainer);
                operatingSystem.RedirectRequestToRequestHandler(dataContainer).Wait();
                if (DefaultUseCaseWasLoaded(dataContainer))
                {
                    operatingSystem.CacheValue<bool>(StateName, true);
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Configures request to load default use case
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        protected virtual void ConfigureRequestToLoadDefaultUseCase(IDataContainer dataContainer)
        {
            dataContainer.DomainName = DomainName;
            dataContainer.ApplicationLayerName = ApplicationLayerName;
            dataContainer.UseCaseName = UseCaseName;
            dataContainer.TransitionName = TransitionName;
        }

        /// <summary>
        /// Gets flag indicating whether default use case was loaded
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>True if default use case was loaded, otherwise False</returns>
        protected virtual bool DefaultUseCaseWasLoaded(IDataContainer dataContainer)
        {
            return dataContainer.Exception == null && string.IsNullOrEmpty(dataContainer.ErrorMessage);
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
        /// Gets or sets state name
        /// </summary>
        protected string StateName
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
