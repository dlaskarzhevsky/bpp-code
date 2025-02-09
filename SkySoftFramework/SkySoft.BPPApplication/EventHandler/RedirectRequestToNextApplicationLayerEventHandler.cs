namespace SkySoft.BPPApplication
{
    /// <summary>
    /// RedirectRequestToNextApplicationLayerEventHandler event handler
    /// </summary>
    public class RedirectRequestToNextApplicationLayerEventHandler : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RedirectRequestToNextApplicationLayerEventHandler()
        {
            EventName = SkySoft.Contracts.EventTypes.REDIRECT_REQUEST_TO_NEXT_APPLICATION_LAYER_EVENT;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles event
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleEventAsync()
        {
            DataContainer.RemoveCurrentRequestMetadta();
            switch (DataContainer.ApplicationLayerName)
            {
                case SkySoft.Contracts.ApplicationLayerNames.BL:
                    DataContainer.AddRequestMetadata(null, null, SkySoft.Contracts.ApplicationLayerNames.DPL, null, null);
                    break;
                case SkySoft.Contracts.ApplicationLayerNames.DPL:
                    DataContainer.AddRequestMetadata(null, null, SkySoft.Contracts.ApplicationLayerNames.DAL, null, null);
                    break;
            }

            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
