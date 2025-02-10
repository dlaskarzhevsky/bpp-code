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
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DNS_SERVER;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.SEARCHING;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            ChangeApplicationLayerName();
            AddMissingRequestMetadata();

            await RedirectRequestToNextApplicationLayer();
        }
        #endregion
    }
}
