using Newtonsoft.Json;
using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides data container serializer functionality
    /// </summary>
    static class DataContainerSerializer
    {
        #region Public Methods
        /// <summary>
        /// Serializes data container
        /// </summary>
        /// <param name="dataContainer">Data container (required)</param>
        /// <returns>String containing serialized data container data</returns>
        public static string Serialize(IDataContainer dataContainer)
        {
            if (dataContainer == null)
            {
                throw new ArgumentNullException(nameof(dataContainer));
            }

            return JsonConvert.SerializeObject(dataContainer);
        }
        #endregion
    }
}
