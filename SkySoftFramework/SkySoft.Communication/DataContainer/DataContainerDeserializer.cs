using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides data container deserializer functionality
    /// </summary>
    static class DataContainerDeserializer
    {
        #region Public Methods
        /// <summary>
        /// Convert JArray into data collection
        /// </summary>
        /// <typeparam name="T">Data collection type</typeparam>
        /// <param name="inputDataCollection">Input data collection</param>
        /// <param name="outputDataCollection">Output data collection</param>
        /// <returns>True if collection is converted from JArray, otherwise false</returns>
        public static bool ConvertJArrayIntoDataCollection<T>(object inputDataCollection, out IDataCollection<T>? outputDataCollection)
        {
            if (inputDataCollection is JArray)
            {
                JArray desirializedDataCollectionJArray = (JArray)inputDataCollection;
                outputDataCollection = desirializedDataCollectionJArray.ToObject<DataCollection<T>>();
                return true;
            }
            else
            {
                outputDataCollection = (IDataCollection<T>)inputDataCollection;
                return false;
            }
        }

        /// <summary>
        /// Deserializes data container
        /// </summary>
        /// <param name="serializedDataContainer">Sting containing serialized data container data</param>
        /// <returns>Deserialized data container or NULL if desirialization not possible</returns>
        public static IDataContainer? Deserialize(string serializedDataContainer)
        {
            if (string.IsNullOrEmpty(serializedDataContainer))
            {
                return default!;
            }

            return JsonConvert.DeserializeObject<DataContainer>(serializedDataContainer);
        }
        #endregion
    }
}
