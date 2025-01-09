using SkySoft.ICommunication;

namespace SkySoft.IBPPApplication
{
    /// <summary>
    /// Defines request controller functionality
    /// </summary>
    public interface IRequestController
    {
        #region Methods
        /// <summary>
        /// Sends request to DNS server
        /// </summary>
        /// <param name="requestDataContainer">Request data container</param>
        /// <returns>Response data container</returns>
        Task<IDataContainer> SendRequestToDnsServer(IDataContainer requestDataContainer);
        #endregion
    }
}
