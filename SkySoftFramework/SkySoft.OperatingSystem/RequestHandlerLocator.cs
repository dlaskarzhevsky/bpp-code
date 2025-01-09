using SkySoft.IBPPApplication;

namespace SkySoft.OperatingSystem
{
    /// <summary>
    /// Provides request handler locator functionality
    /// </summary>
    class RequestHandlerLocator
    {
        #region Public Methods
        /// <summary>
        /// Finds request handler
        /// </summary>
        /// <param name="requestHandlers">Request handlers</param>
        /// <param name="key">Request handler key</param>
        /// <returns>Found request handler or NULL</returns>
        public static IRequestHandler? FindRequestHandler(IEnumerable<IRequestHandler> requestHandlers, string? key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            IRequestHandler? foundRequestHandler = null;
            foreach (IRequestHandler requestHandler in requestHandlers)
            {
                if (requestHandler.Key == key)
                {
                    foundRequestHandler = requestHandler;
                    break;
                }
            }

            return foundRequestHandler;
        }
        #endregion
    }
}
