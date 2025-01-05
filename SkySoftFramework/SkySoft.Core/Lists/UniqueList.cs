using System;
using System.Collections.Generic;

namespace SkySoft.Core
{
    /// <summary>
    /// Provides unique list functionality
    /// </summary>
    /// <typeparam name="T">Entry type</typeparam>
    public class UniqueList<T> : List<T> where T : IUniqueListEntry
    {
        #region Methods
        /// <summary>
        /// Adds item to list or returns existing if exists
        /// </summary>
        /// <param name="item">Item for adding</param>
        /// <returns>Added item or found one with specified key</returns>
        public new T Add(T item)
        {
            // Verifying whether item with specified key exists
            T existingItem = Find(item.Key);
            if (existingItem == null)
            {
                // Adding item
                base.Add(item);
                return item;
            }

            return existingItem;
        }

        /// <summary>
        /// Finds item with specified key
        /// </summary>
        /// <param name="key">Item key</param>
        /// <returns></returns>
        protected T Find(string key)
        {
            // Validate arguments
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            for (int i = 0; i < base.Count; i++)
            {
                if (base[i].Key == key)
                {
                    return base[i];
                }
            }

            return default(T);
        }

        /// <summary>
        /// Removes item
        /// </summary>
        /// <param name="key">Item key (required)</param>
        /// <returns>Removed item or null if list doesn't contain item with specified key</returns>
        public T Remove(string key)
        {
            // Verifying whether item with specified key exists
            T existingItem = Find(key);
            if (existingItem == null)
            {
                return default(T);
            }

            base.Remove(existingItem);
            return existingItem;
        }
        #endregion
    }
}
