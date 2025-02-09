using SkySoft.Communication;

namespace SkySoft.DnsServer.BL
{
    /// <summary>
    /// RegisteringHost transition request handler
    /// </summary>
    public partial class RegisteringHost : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisteringHost()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DNS_SERVER;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.REGISTERING_HOST;
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
