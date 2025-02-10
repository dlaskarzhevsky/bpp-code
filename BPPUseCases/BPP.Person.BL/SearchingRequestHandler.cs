namespace BPP.Person.BL
{
    /// <summary>
    /// Provides searching request handler functionality
    /// </summary>
    public partial class SearchingRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchingRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.BPP;
            UseCaseName = BPP.Person.CON.UseCaseContract.PERSON;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
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
            await RedirectRequestToNextApplicationLayer();
        }
        #endregion
    }
}
