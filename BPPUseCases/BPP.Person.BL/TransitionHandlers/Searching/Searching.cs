namespace BPP.Person.BL
{
    /// <summary>
    /// Provides searching request handler functionality
    /// </summary>
    public partial class Searching : SkySoft.BPPApplication.BusinessLogicRequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Searching()
        {
            DomainName = SkySoft.Contracts.DomainNames.BPP;
            UseCaseName = BPP.Person.CON.UseCaseContract.PERSON;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            StateName = BPP.Person.CON.StateTypes.INITIAL;
            TransitionName = BPP.Person.CON.TransitionTypes.SEARCHING;
        }
        #endregion
    }
}
