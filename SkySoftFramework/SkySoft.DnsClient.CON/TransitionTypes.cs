namespace SkySoft.DnsClient.CON
{
    /// <summary>
    /// Provides transition types functionality
    /// </summary>
    public class TransitionTypes : SkySoft.Contracts.TransitionTypes
    {
        /// <summary>
        /// Registering DNS client with DNS server
        /// </summary>
        public const string REGISTERING_DNS_CLIENT_WITH_DNS_SERVER = "RegisteringDnsClientWithDnsServer";

        /// <summary>
        /// Sending request to DNS server transition
        /// </summary>
        public const string SENDING_REQUEST_TO_DNS_SERVER = "SendingRequestToDnsServer";
    }
}
