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
        #endregion
    }
}
