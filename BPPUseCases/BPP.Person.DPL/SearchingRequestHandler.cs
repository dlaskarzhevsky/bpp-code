namespace BPP.Person.DPL
{
    /// <summary>
    /// Provides searching request handler functionality
    /// </summary>
    public class SearchingRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchingRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.BPP;
            UseCaseName = BPP.Person.CON.UseCaseContract.PERSON;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            StateName = BPP.Person.CON.StateTypes.INITIAL;
            TransitionName = BPP.Person.CON.TransitionTypes.SEARCHING;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequestAsync()
        {
            await RaiseDataRequestEvent();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Raises DataRequest event
        /// </summary>
        async Task RaiseDataRequestEvent()
        {
            DataContainer!.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.BPP,
                BPP.Person.CON.UseCaseContract.PERSON,
                SkySoft.Contracts.ApplicationLayerNames.DPL,
                BPP.Person.CON.StateTypes.INITIAL,
                SkySoft.Contracts.EventTypes.DATA_REQUEST_EVENT);
            DataContainer = await RaiseEvent(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
