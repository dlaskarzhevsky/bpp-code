namespace SkySoft.DnsServer.BL
{
    /// <summary>
    /// RegisteringHost transition request handler
    /// </summary>
    public partial class RegisteringHost
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
