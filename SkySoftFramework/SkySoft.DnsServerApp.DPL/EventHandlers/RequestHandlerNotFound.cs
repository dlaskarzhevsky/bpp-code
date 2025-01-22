using SkySoft.ICommunication;

namespace SkySoft.DnsServerApp.DPL
{
    /// <summary>
    /// RequestHandlerNotFound event handler
    /// </summary>
    public class RequestHandlerNotFound : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RequestHandlerNotFound()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.Contracts.UseCaseTypes.APPLICATION;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.NFA;
            EventName = SkySoft.Contracts.EventTypes.REQUEST_HANDLER_NOT_FOUND_EVENT;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles event
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override void HandleEvent()
        {
            string errorMessage = $"Cannot handle request with the following metadata" + Environment.NewLine + $"Application layer full name: {DataContainer.DomainName}_{DataContainer.UseCaseName}_{DataContainer.ApplicationLayerName}, State name: {DataContainer.StateName}, Transition name: {DataContainer.TransitionName}";
            DataContainer.SetMessage(errorMessage, MessageType.Error);
            DataContainer.RequestHandled = true;
        }
        #endregion
    }
}
