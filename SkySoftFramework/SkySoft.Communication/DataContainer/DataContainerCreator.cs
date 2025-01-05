using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    ///Provides data container creator functionality
    /// </summary>
    static class DataContainerCreator
    {
        #region Public Methods
        /// <summary>
        /// Creates data container
        /// </summary>
        /// <returns>Data container</returns>
        public static IDataContainer Create()
        {
            DataContainer dataContainer = new DataContainer();

            IDataCollection<RequestMetadataDTO> requestMetadataDataCollection = new DataCollection<RequestMetadataDTO>();
            dataContainer.AddDataCollection(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA, requestMetadataDataCollection);

            RequestMetadataDTO requestMetadataDTO = new RequestMetadataDTO();
            requestMetadataDataCollection.Add(requestMetadataDTO);

            return dataContainer;
        }
        #endregion
    }
}
