namespace SkySoft.DnsServer.BL
{
    /// <summary>
    /// Searching transition request handler
    /// </summary>
    public partial class Searching : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Searching()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.BL;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.REGISTERING_HOST;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            await RedirectRequestToNextApplicationLayer();
        }
        #endregion
    }
}
