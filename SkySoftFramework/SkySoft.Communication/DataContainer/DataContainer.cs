using SkySoft.Contracts;
using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides data container functionality
    /// </summary>
    public class DataContainer : Dictionary<string, dynamic>, IDataContainer
    {
        #region Static Methods
        /// <summary>
        /// Creates data container
        /// </summary>
        /// <returns>Data container</returns>
        public static IDataContainer CreateDataContainer()
        {
            return DataContainerCreator.Create();
        }

        /// <summary>
        /// Deserializes data container
        /// </summary>
        /// <param name="serializedDataContainer">Sting containing serialized data container data</param>
        /// <returns>Deserialized data container or NULL if desirialization not possible</returns>
        public static IDataContainer? Deserialize(string serializedDataContainer)
        {
            IDataContainer? dataContainer = DataContainerDeserializer.Deserialize(serializedDataContainer);
            if (dataContainer != null)
            {
                // The following line of code deserializes request metadata by restoring it from JArray object
                dataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
                dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            }

            return dataContainer;
        }

        /// <summary>
        /// Gets new data transfer object
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <returns>New data transfer object</returns>
        public static T GetNewDataTransferObject<T>()
        {
            T? newDTO = default!;
            Type type = typeof(T);
            if (type.IsInterface)
            {
                string typeName = type.Name.Substring(1);
                Type? classType = Type.GetType(typeName);
                if (classType == null)
                {
                    throw new TypeAccessException("Cannot create new data transfer object type of " + typeName);
                }

                newDTO = (T?)Activator.CreateInstance(classType);
            }
            else
            {
                newDTO = Activator.CreateInstance<T>();
            }

            if (newDTO == null)
            {
                throw new TypeAccessException("Cannot create new data transfer object");
            }

            IDataTransferObject dataTransferObject = (IDataTransferObject)newDTO;
            dataTransferObject.DateOfCreation = DateTime.Now;
            dataTransferObject.DateOfModification = dataTransferObject.DateOfCreation;

            return newDTO;
        }

        /// <summary>
        /// Serializes data container
        /// </summary>
        /// <param name="dataContainer">Data container (required)</param>
        /// <returns>String containing serialized data container data</returns>
        public static string Serialize(IDataContainer dataContainer)
        {
            return DataContainerSerializer.Serialize(dataContainer);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds data collection to data container by replacing existing data collection
        /// IDataContainer interface implementation
        /// </summary>
        /// <typeparam name="T">Data collection type</typeparam>
        /// <param name="key">Data collection key (required)</param>
        /// <param name="dataCollection">Data collection for adding (required)</param>
        public void AddDataCollection<T>(string key, IDataCollection<T> dataCollection)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (dataCollection == null)
            {
                throw new ArgumentNullException(nameof(dataCollection));
            }

            if (ContainsKey(key))
            {
                Remove(key);
            }

            Add(key, dataCollection);
        }

        /// <summary>
        /// Adds request metadata
        /// IDataContainer interface implementation
        /// </summary>
        /// <param name="requestMetadataDTO">Request metadata</param>
        public void AddRequestMetadata(IRequestMetadataDTO requestMetadataDTO)
        {
            RequestMetadataDataCollection.AddRequestMetadata(this, (RequestMetadataDTO)requestMetadataDTO);
        }

        /// <summary>
        /// Adds request metadata
        /// IDataContainer interface implementation
        /// </summary>
        /// <param name="domainName">Domain name</param>
        /// <param name="useCaseName">Application layer name</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <param name="stateName">State name</param>
        /// <param name="transitionName">Transition name</param>
        public void AddRequestMetadata(string? domainName, string? useCaseName, string? applicationLayerName, string? stateName, string? transitionName)
        {
            RequestMetadataDataCollection.AddRequestMetadata(this, domainName, useCaseName, applicationLayerName, stateName, transitionName);
        }

        /// <summary>
        /// Clears messages
        /// IDataContainer interface implementation
        /// </summary>
        public void ClearMessages()
        {
            ExceptionDataCollection.ClearMessages(this);
        }

        /// <summary>
        /// Gets data collection with specified key from data container returning NULL if not found
        /// IDataContainer interface implementation
        /// </summary>
        /// <typeparam name="T">Data collection type</typeparam>
        /// <param name="key">Data collection key (required)</param>
        /// <returns>Data collection if found, otherwise NULL</returns>
        public IDataCollection<T>? GetDataColletion<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDataCollection<T>? dataCollection = null;
            if (ContainsKey(key))
            {
                if (DataContainerDeserializer.ConvertJArrayIntoDataCollection<T>(this[key], out dataCollection))
                {
                    if (dataCollection != null)
                    {
                        Remove(key);
                        AddDataCollection(key, dataCollection);
                    }
                }
            }
            else
            {
                dataCollection = new DataCollection<T>();
                AddDataCollection(key, dataCollection);
            }

            return dataCollection;
        }

        /// <summary>
        /// Gets last data transfer object by removing it from data collection
        /// IDataContainer interface implementation
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <param name="key">Data collection key (required)</param>
        /// <returns>Last data transfer object in data collection</returns>
        public T? GetLastDTOByRemovingItFromDataCollection<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDataCollection<T>? dataCollection = GetDataColletion<T>(key);
            if (dataCollection == null)
            {
                return default!;
            }

            T? dataTransferObject = default!;
            if (dataCollection.Count > 0)
            {
                dataTransferObject =dataCollection[dataCollection.Count - 1];
                dataCollection.Remove(dataTransferObject);
            }

            return dataTransferObject;
        }

        /// <summary>
        /// Gets last data transfer object in data collection
        /// IDataContainer interface implementation
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <param name="key">Data collection key (required)</param>
        /// <returns>Last data transfer object in data collection</returns>
        public T? GetLastDTOFromDataCollection<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDataCollection<T>? dataCollection = GetDataColletion<T>(key);
            if (dataCollection == null)
            {
                return default!;
            }

            if (dataCollection.Count > 0)
            {
                return dataCollection[dataCollection.Count - 1];
            }
            else
            {
                return default!;
            }
        }

        /// <summary>
        /// Gets new data transfer object
        /// IDataContainer interface implementation
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <returns>New data transfer object</returns>
        public T GetNewDTO<T>()
        {
            return DataContainer.GetNewDataTransferObject<T>();
        }

        /// <summary>
        /// Gets new data transfer object from data collection
        /// IDataContainer interface implementation
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <param name="key">Data collection key (required)</param>
        /// <returns>New data transfer object</returns>
        public T GetNewDTO<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDataCollection<T>? dataCollection = GetDataColletion<T>(key);
            if (dataCollection == null)
            {
                return default!;
            }

            T newDTO = GetNewDTO<T>(dataCollection);
            return newDTO;
        }

        /// <summary>
        /// Gets new data transfer object
        /// IDataContainer interface implementation
        /// </summary>
        /// <typeparam name="T">Data transfer object type</typeparam>
        /// <param name="dataCollection">Data collection to which new data transfer object belongs</param>
        /// <returns>New data transfer object</returns>
        public T GetNewDTO<T>(IDataCollection<T>? dataCollection)
        {
            T? newDTO = DataContainer.GetNewDataTransferObject<T>();
            if (dataCollection != null)
            {
                dataCollection.Add(newDTO);
            }

            return newDTO;
        }


        /// <summary>
        /// Removes current request metadta
        /// IDataContainer interface implementation
        /// </summary>
        /// <returns>Removed request metadata</returns>
        public IRequestMetadataDTO? RemoveCurrentRequestMetadta()
        {
            return RequestMetadataDataCollection.RemoveCurrentRequestMetadta(this);
        }

        /// <summary>
        /// Removes data collection if found
        /// IDataContainer interface implementation
        /// </summary>
        /// <param name="key">Data collection key (required)</param>
        public void RemoveDataCollection(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (ContainsKey(key))
            {
                Remove(key);
            }
        }

        /// <summary>
        /// Removes last data transfer object from data collection
        /// IDataContainer interface implementation
        /// </summary>
        /// <param name="key">Data collection key (required)</param>
        public void RemoveLastDTOFromDataCollection<T>(string key)
        {
            GetLastDTOByRemovingItFromDataCollection<T>(key);
        }

        /// <summary>
        /// Sets message
        /// IDataContainer interface implementation
        /// </summary>
        /// <param name="message">Message text</param>
        /// <param name="messageType">Message type</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <param name="applicationLayerUrl">Application layer URL</param>
        public void SetMessage(string message, MessageType messageType, string? applicationLayerName, string? applicationLayerUrl)
        {
            ExceptionDataCollection.AddMessage(this, message, messageType, applicationLayerName, applicationLayerUrl);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets application layer name
        /// IDataContainer interface implementation
        /// </summary>
        public string? ApplicationLayerName
        {
            get
            {
                return RequestMetadataDataCollection.GetApplicationLayerName(this);
            }
            set
            {
                RequestMetadataDataCollection.SetApplicationLayerName(this, value);
            }
        }

        /// <summary>
        /// Gets or sets application layer full name
        /// IDataContainer interface implementation
        /// </summary>
        public string? ApplicationLayerFullName
        {
            get
            {
                return RequestMetadataDataCollection.GetApplicationLayerFullName(this);
            }
        }

        /// <summary>
        /// Gets or sets domain name
        /// IDataContainer interface implementation
        /// </summary>
        public string? DomainName
        {
            get
            {
                return RequestMetadataDataCollection.GetDomainName(this);
            }
            set
            {
                RequestMetadataDataCollection.SetDomainName(this, value);
            }
        }

        /// <summary>
        /// Gets message
        /// IDataContainer interface implementation
        /// </summary>
        public string? Message
        {
            get
            {
                return ExceptionDataCollection.GetMessage(this);
            }
        }

        /// <summary>
        /// Gets message type
        /// IDataContainer interface implementation
        /// </summary>
        public MessageType MessageType
        {
            get
            {
                return ExceptionDataCollection.GetMessageType(this);
            }
        }

        /// <summary>
        /// Gets or sets exception
        /// IDataContainer interface implementation
        /// </summary>
        public Exception? Exception
        {
            get
            {
                return ExceptionDataCollection.GetException(this);
            }
            set
            {
                ExceptionDataCollection.AddException(this, value);
            }
        }

        /// <summary>
        /// IDataContainer interface implementation
        /// Gets or sets flag indicating whether request handled
        /// </summary>
        public bool RequestHandled
        {
            get
            {
                return RequestMetadataDataCollection.GetRequestHandled(this);
            }
            set
            {
                RequestMetadataDataCollection.SetRequestHandled(this, value);
            }
        }

        /// <summary>
        /// Gets or sets state name
        /// IDataContainer interface implementation
        /// </summary>
        public string? StateName
        {
            get
            {
                return RequestMetadataDataCollection.GetStateName(this);
            }
            set
            {
                RequestMetadataDataCollection.SetStateName(this, value);
            }
        }

        /// <summary>
        /// Gets or sets transition name
        /// IDataContainer interface implementation
        /// </summary>

        public string? TransitionName
        {
            get
            {
                return RequestMetadataDataCollection.GetTransitionName(this);
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    RequestMetadataDataCollection.SetTransitionName(this, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets application layer name
        /// IDataContainer interface implementation
        /// </summary>
        public string? UseCaseName
        {
            get
            {
                return RequestMetadataDataCollection.GetUseCaseName(this);
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    RequestMetadataDataCollection.SetUseCaseName(this, value);
                }
            }
        }
        #endregion
    }
}
