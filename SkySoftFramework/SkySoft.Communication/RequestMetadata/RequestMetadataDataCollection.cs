using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides request metadata data collection functionality
    /// </summary>
    static class RequestMetadataDataCollection
    {
        #region Public Methods
        /// <summary>
        /// Gets application layer name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Application layer name</returns>
        public static string? GetApplicationLayerName(IDataContainer dataContainer)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            return requestMetadata.ApplicationLayerName;
        }

        /// <summary>
        /// Gets application layer full name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Application layer full name</returns>
        public static string? GetApplicationLayerFullName(IDataContainer dataContainer)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            return requestMetadata.ApplicationLayerFullName;
        }

        /// <summary>
        /// Gets application domain name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Domain name</returns>
        public static string? GetDomainName(IDataContainer dataContainer)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            return requestMetadata.DomainName;
        }

        /// <summary>
        /// Gets flag indicating whether request handled
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Flag indicating whether request handled</returns>
        public static bool GetRequestHandled(IDataContainer dataContainer)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            return requestMetadata.RequestHandled;
        }

        /// <summary>
        /// Gets state name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>State name</returns>
        public static string? GetStateName(IDataContainer dataContainer)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            return requestMetadata.StateName;
        }

        /// <summary>
        /// Gets transition name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Transition name</returns>
        public static string? GetTransitionName(IDataContainer dataContainer)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            return requestMetadata.TransitionName;
        }

        /// <summary>
        /// Gets application layer name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Application layer name</returns>
        public static string? GetUseCaseName(IDataContainer dataContainer)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            return requestMetadata.UseCaseName;
        }

        /// <summary>
        /// Adds request metadata
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="domainName">Domain name</param>
        /// <param name="useCaseName">Application layer name</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <param name="stateName">State name</param>
        /// <param name="transitionName">Transition name</param>
        public static void AddRequestMetadata(IDataContainer dataContainer, string? domainName, string? useCaseName, string? applicationLayerName, string? stateName, string? transitionName)
        {
            if (string.IsNullOrEmpty(applicationLayerName) && string.IsNullOrEmpty(domainName) && string.IsNullOrEmpty(useCaseName) && string.IsNullOrEmpty(stateName) && string.IsNullOrEmpty(transitionName))
            {
                throw new ArgumentOutOfRangeException("One of the five arguments is required");
            }

            IDataCollection<RequestMetadataDTO>? requestMetadataDataCollection = dataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
            if (requestMetadataDataCollection == null)
            {
                return;
            }

            RequestMetadataDTO previousRequestMetadataDTO = requestMetadataDataCollection[requestMetadataDataCollection.Count - 1];
            RequestMetadataDTO requestMetadataDTO = new RequestMetadataDTO();
            if (string.IsNullOrEmpty(applicationLayerName))
            {
                requestMetadataDTO.ApplicationLayerName = previousRequestMetadataDTO.ApplicationLayerName;
            }
            else
            {
                requestMetadataDTO.ApplicationLayerName = applicationLayerName;
            }

            if (string.IsNullOrEmpty(domainName))
            {
                requestMetadataDTO.DomainName = previousRequestMetadataDTO.DomainName;
            }
            else
            {
                requestMetadataDTO.DomainName = domainName;
            }

            if (string.IsNullOrEmpty(useCaseName))
            {
                requestMetadataDTO.UseCaseName = previousRequestMetadataDTO.UseCaseName;
            }
            else
            {
                requestMetadataDTO.UseCaseName = useCaseName;
            }

            if (stateName == null)
            {
                requestMetadataDTO.StateName = previousRequestMetadataDTO.StateName;
            }
            else
            {
                requestMetadataDTO.StateName = stateName;
            }

            if (string.IsNullOrEmpty(transitionName))
            {
                requestMetadataDTO.TransitionName = previousRequestMetadataDTO.TransitionName;
            }
            else
            {
                requestMetadataDTO.TransitionName = transitionName;
            }

            requestMetadataDataCollection.Add(requestMetadataDTO);
        }

        /// <summary>
        /// Removes current request metadta
        /// <param name="dataContainer">Data container</param>
        /// </summary>
        public static void RemoveCurrentRequestMetadta(IDataContainer dataContainer)
        {
            IDataCollection<RequestMetadataDTO>? requestMetadataDataCollection = dataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
            if (requestMetadataDataCollection == null || requestMetadataDataCollection.Count < 2)
            {
                return;
            }

            requestMetadataDataCollection.RemoveAt(requestMetadataDataCollection.Count - 1);
        }

        /// <summary>
        /// Sets application layer name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="applicationLayerName">Application layer name</param>
        public static void SetApplicationLayerName(IDataContainer dataContainer, string? applicationLayerName)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            requestMetadata.ApplicationLayerName = applicationLayerName;
        }

        /// <summary>
        /// Sets application domain name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="domainName">Domain name</param>
        public static void SetDomainName(IDataContainer dataContainer, string? domainName)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            requestMetadata.DomainName = domainName;
        }

        /// <summary>
        /// Sets flag indicating whether request handled
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="requestHandled">State name</param>
        public static void SetRequestHandled(IDataContainer dataContainer, bool requestHandled)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            requestMetadata.RequestHandled = requestHandled;
        }

        /// <summary>
        /// Sets state name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="stateName">State name</param>
        public static void SetStateName(IDataContainer dataContainer, string? stateName)
        {
            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            requestMetadata.StateName = stateName;
        }

        /// <summary>
        /// Sets transition name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="transitionName">Transition name (required)</param>
        public static void SetTransitionName(IDataContainer dataContainer, string transitionName)
        {
            if (string.IsNullOrEmpty(transitionName))
            {
                throw new ArgumentNullException(nameof(transitionName));
            }

            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            requestMetadata.TransitionName = transitionName;
        }

        /// <summary>
        /// Sets application layer name
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="useCaseName">Application layer name (required)</param>
        public static void SetUseCaseName(IDataContainer dataContainer, string useCaseName)
        {
            if (string.IsNullOrEmpty(useCaseName))
            {
                throw new ArgumentNullException(nameof(useCaseName));
            }

            IRequestMetadataDTO requestMetadata = GetRequestMetadata(dataContainer);
            requestMetadata.UseCaseName = useCaseName;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get request metadata
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Request metadata</returns>
        static IRequestMetadataDTO GetRequestMetadata(IDataContainer dataContainer)
        {
            IDataCollection<RequestMetadataDTO>? requestMetadataDataCollection = dataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
            if (requestMetadataDataCollection == null)
            {
                return default!;
            }

            return requestMetadataDataCollection[requestMetadataDataCollection.Count - 1];
        }
        #endregion
    }
}
