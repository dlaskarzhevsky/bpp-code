using SkySoft.ICommunication;

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
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = BPP.Person.CON.UseCaseContract.PERSON;
            StateName = BPP.Person.CON.StateTypes.INITIAL;
            TransitionName = BPP.Person.CON.TransitionTypes.SEARCHING;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Processes request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public override async Task<IDataContainer> ProcessRequest(IDataContainer dataContainer)
        {
            dataContainer = await RedirectRequestToNextApplicationLayer(dataContainer, SkySoft.Contracts.ApplicationLayerNames.DAL);
            return dataContainer;
        }
        #endregion
    }
}
