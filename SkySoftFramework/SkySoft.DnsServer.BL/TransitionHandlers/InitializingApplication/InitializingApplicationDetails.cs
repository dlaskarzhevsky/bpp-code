namespace SkySoft.DnsServer.BL
{
    /// <summary>
    /// InitializingApplication transition request handler
    /// </summary>
    public partial class InitializingApplication
    {
        #region Private Methods
        /// <summary>
        /// Loads DNS records from storage into cache
        /// </summary>
        async Task LoadDnsRecordsFromStorageIntoCache()
        {
            await RaiseEvent(SkySoft.DnsServer.CON.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT);
        }

        /// <summary>
        /// Switches application into initial state
        /// </summary>
        void SwitchApplicationIntoInitialState()
        {
            DataContainer.StateName = SkySoft.Contracts.StateTypes.INITIAL;
            OperatingSystem.CacheValue<string>($"{DataContainer.ApplicationLayerFullName}_ApplicationState", DataContainer.StateName);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets flag indicating whether application initialized
        /// </summary>
        bool ApplicationInitialized
        {
            get
            {
                return OperatingSystem.GetValueFomCache<string>($"{DataContainer.ApplicationLayerFullName}_ApplicationState") == SkySoft.Contracts.StateTypes.INITIAL;
            }
        }
        #endregion
    }
}
