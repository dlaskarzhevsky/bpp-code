namespace SkySoft.DnsClient.CON
{
    /// <summary>
    /// Provides event types
    /// </summary>
    public class EventTypes : SkySoft.Contracts.EventTypes
    {
        /// <summary>
        /// DnsClientInitialized event
        /// </summary>
        public const string DNS_CLIENT_INITIALIZED_EVENT = "DnsClientInitializedEvent";

        /// <summary>
        /// Registering DNS client with DNS server
        /// </summary>
        public const string REGISTERING_DNS_CLIENT_WITH_DNS_SERVER_EVENT = "RegisteringDnsClientWithDnsServerEvent";
    }
}
