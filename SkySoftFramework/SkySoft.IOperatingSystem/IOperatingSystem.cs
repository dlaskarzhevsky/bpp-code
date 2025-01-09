using SkySoft.ICommunication;

namespace SkySoft.IOperatingSystem
{
    /// <summary>
    /// Defines operating system functionality
    /// </summary>
    public interface IOS
    {
        #region Methods
        /// <summary>
        /// Caches value
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="value">Value for caching</param>
        void CacheValue<T>(string key, T value);

        /// <summary>
        /// Gets value from application configuration
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <returns>Value from application configuration</returns>
        T? GetValueFromApplicationConfiguration<T>(string key);

        /// <summary>
        /// Get value fom cache
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <returns>Value fom cache</returns>
        T? GetValueFomCache<T>(string key);

        /// <summary>
        /// Redirects request to request handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer);
        #endregion
    }
}
