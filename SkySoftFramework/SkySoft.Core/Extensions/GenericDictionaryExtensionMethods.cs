using System.Collections.Generic;
using System.Linq;

namespace SkySoft.Core
{
    /// <summary>
    /// Contains Dictionary<,> extention methods
    /// </summary>
    public static class GenericDictionaryExtensionMethods
    {
        #region Methods
        /// <summary>
        /// Returns key at specified position
        /// </summary>
        /// <typeparam name="K">Key type</typeparam>
        /// <typeparam name="V">Value type</typeparam>
        /// <param name="dictionary">Dictionary instance</param>
        /// <param name="position">Value position</param>
        /// <returns>Value at specified position</returns>
        public static K KeyAt<K, V>(this Dictionary<K, V> dictionary, int position)
        {
            return dictionary.Keys.ElementAtOrDefault(position);
        }

        /// <summary>
        /// Returns value at specified position
        /// </summary>
        /// <typeparam name="K">Key type</typeparam>
        /// <typeparam name="V">Value type</typeparam>
        /// <param name="dictionary">Dictionary instance</param>
        /// <param name="position">Value position</param>
        /// <returns>Value at specified position</returns>
        public static V ValueAt<K, V>(this Dictionary<K, V> dictionary, int position)
        {
            return dictionary.Values.ElementAtOrDefault(position);
        }
        #endregion
    }
}
