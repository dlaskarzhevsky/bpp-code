using SkySoft.Core;
using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides data container functionality
    /// </summary>
    public class DataContainer : Dictionary<string, dynamic>, IDataContainer
    {
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
        /// <param name="applicationLayerName">Application layer name</param>
        /// <param name="domainName">Domain name</param>
        /// <param name="useCaseName">Application layer name</param>
        /// <param name="stateName">State name</param>
        /// <param name="transitionName">Transition name</param>
        public void AddRequestMetadata(string? applicationLayerName, string? domainName, string? useCaseName, string? stateName, string? transitionName)
        {
            RequestMetadataDataCollection.AddRequestMetadata(this, applicationLayerName, domainName, useCaseName, stateName, transitionName);
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
            if (!ContainsKey(key))
            {
                dataCollection = new DataCollection<T>();
                AddDataCollection(key, dataCollection);
            }
            else
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

            return dataCollection;
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
            T? newDTO = (T?)Activator.CreateInstance(typeof(T));
            if (newDTO == null)
            {
                throw new TypeAccessException("Cannot create new data transfer object");
            }

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
        public void RemoveCurrentRequestMetadta()
        {
            RequestMetadataDataCollection.RemoveCurrentRequestMetadta(this);
        }

        /// <summary>
        /// Removes data collection
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
        #endregion

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
            }

            return dataContainer;
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
    }
}
