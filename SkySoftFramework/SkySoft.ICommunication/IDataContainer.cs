namespace SkySoft.ICommunication
{
    /// <summary>
    /// Defines data container functionality
    /// </summary>
    public interface IDataContainer
    {
        #region Properties
        /// <summary>
        /// Gets or sets application layer name
        /// </summary>
        string? ApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets domain name
        /// </summary>
        string? DomainName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets state name
        /// </summary>
        string? StateName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets transition name
        /// </summary>
        string? TransitionName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets application layer name
        /// </summary>
        string? UseCaseName
        {
            get;
            set;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds data collection to data container by replacing existing data collection
        /// </summary>
        /// <typeparam name="T">Data collection type</typeparam>
        /// <param name="key">Data collection key (required)</param>
        /// <param name="dataCollection">Data collection for adding (required)</param>
        void AddDataCollection<T>(string key, IDataCollection<T> dataCollection);

        /// <summary>
        /// Adds request metadata
        /// </summary>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <param name="domainName">Domain name</param>
        /// <param name="useCaseName">Use case name</param>
        /// <param name="stateName">State name</param>
        /// <param name="transitionName">Transition name</param>
        void AddRequestMetadata(string? applicationLayerName, string? domainName, string? useCaseName, string? stateName, string? transitionName);

        /// <summary>
        /// Gets data collection with specified key from data container returning NULL if not found
        /// </summary>
        /// <typeparam name="T">Data collection type</typeparam>
        /// <param name="key">Data collection key (required)</param>
        /// <returns>Data collection if found, otherwise NULL</returns>
        IDataCollection<T>? GetDataColletion<T>(string key);

        #region Methods
        /// <summary>
        /// Gets new data transfer object
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <param name="dataCollection">Data collection to which new data transfer object belongs</param>
        /// <returns>New data transfer object</returns>
        T GetNewDTO<T>(IDataCollection<T> dataCollection);
        #endregion

        /// <summary>
        /// Removes data collection
        /// </summary>
        /// <param name="key">Data collection key (required)</param>
        void RemoveDataCollection(string key);

        /// <summary>
        /// Removes current request metadta
        /// </summary>
        void RemoveCurrentRequestMetadta();
        #endregion
    }
}
