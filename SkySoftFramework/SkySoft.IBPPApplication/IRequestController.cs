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
        /// Redirect request to request handler
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        Task<IDataContainer> RedirectRequestToRequestHandler(IDataContainer dataContainer);
        #endregion
    }
}
