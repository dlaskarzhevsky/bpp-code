namespace SkySoft.Contracts
{
    /// <summary>
    /// Provides event types
    /// </summary>
    public class EventTypes
    {
        /// <summary>
        /// DnsServerDataRequest event
        /// </summary>
        public const string DNS_SERVER_DATA_REQUEST_EVENT = "DnsServerDataRequestEvent";

        /// <summary>
        /// RedirectRequestToNextApplicationLayer event
        /// </summary>
        public const string REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT = "RedirectRequestToNextApplicationLayerEvent";

        /// <summary>
        /// RemoteServerDataRequest event
        /// </summary>
        public const string REMOTE_SERVER_DATA_REQUEST_EVENT = "RemoteServerDataRequestEvent";

        /// <summary>
        /// RequestHandlerNotFound event
        /// </summary>
        public const string REQUEST_HANDLER_NOT_FOUND_EVENT = "RequestHandlerNotFoundEvent";
    }
}
