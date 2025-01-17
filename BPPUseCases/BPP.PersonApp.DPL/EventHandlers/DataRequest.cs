namespace BPP.PersonApp.DPL
{
    /// <summary>
    /// HostInitialized event handler
    /// </summary>
    public class DataRequest : SkySoft.BPPApplication.EventHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public DataRequest()
        {
            DomainName = SkySoft.Contracts.DomainNames.BPP;
            UseCaseName = BPP.Person.CON.UseCaseContract.PERSON;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            EventName = SkySoft.Contracts.EventTypes.DATA_REQUEST_EVENT;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles event
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleEvent()
        {
            DataContainer.AddRequestMetadata(
                SkySoft.Contracts.DomainNames.BPP,
                BPP.Person.CON.UseCaseContract.PERSON,
                SkySoft.Contracts.ApplicationLayerNames.DAL,
                BPP.Person.CON.StateTypes.INITIAL,
                BPP.Person.CON.TransitionTypes.SEARCHING);

            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
