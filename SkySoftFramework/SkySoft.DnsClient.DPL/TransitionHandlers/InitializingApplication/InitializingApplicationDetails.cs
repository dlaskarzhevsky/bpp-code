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

        /// <summary>
        /// Redirect request to next application layer
        /// </summary>
        async Task RedirectRequestToNextApplicationLayer()
        {
            await RaiseEvent(SkySoft.Contracts.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT, false);
        }

        /// <summary>
        /// Updates DNS client data by registration result
        /// </summary>
        async Task UpdateDnsClientDataByRegistrationResult()
        {
            await RaiseEvent(SkySoft.Contracts.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT, false);
        }
        #endregion
    }
}
