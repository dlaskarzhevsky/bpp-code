namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// SaaSSavingRemoteServerData transition request handler
    /// </summary>
    public partial class SaaSSavingRemoteServerData
    {
        #region Private Methods
        /// <summary>
        /// Redirects request to data processing logic
        /// </summary>
        async Task RedirectRequestToDataProcessingLogic()
        {
            DataContainer.AddRequestMetadata(null, null, SkySoft.Contracts.ApplicationLayerNames.DPL, null, null);
            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
