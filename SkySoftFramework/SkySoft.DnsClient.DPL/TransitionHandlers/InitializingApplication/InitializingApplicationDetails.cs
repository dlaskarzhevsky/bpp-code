namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Registers DNS client with DNS server
        /// </summary>
        async Task RegisterDnsClientWithDnsServer()
        {
            await RaiseEvent(SkySoft.DnsClient.CON.EventTypes.REGISTER_DNS_CLIENT_WITH_DNS_SERVER_EVENT);
        }
        #endregion
    }
}
