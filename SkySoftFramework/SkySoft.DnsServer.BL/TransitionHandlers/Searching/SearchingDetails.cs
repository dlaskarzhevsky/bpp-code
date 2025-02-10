namespace SkySoft.DnsServer.BL
{
    /// <summary>
    /// Searching transition request handler
    /// </summary>
    public partial class Searching
    {
        #region Private Methods
        /// <summary>
        /// Adds missing request metadata
        /// </summary>
        void AddMissingRequestMetadata()
        {
            DataContainer.UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            DataContainer.StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
        }

        /// <summary>
        /// Changes application layer name
        /// </summary>
        void ChangeApplicationLayerName()
        {
            DataContainer.ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
        }

        /// <summary>
        /// Redirects request to the next application layer
        /// </summary>
        async Task RedirectRequestToNextApplicationLayer()
        {
            await RaiseEvent(SkySoft.Contracts.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT, false);
        }
        #endregion
    }
}
