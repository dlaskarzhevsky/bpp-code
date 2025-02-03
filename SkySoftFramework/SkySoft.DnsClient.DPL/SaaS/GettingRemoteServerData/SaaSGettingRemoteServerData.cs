
namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// SaaSGettingRemoteServerData transition request handler
    /// </summary>
    public partial class SaaSGettingRemoteServerData : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SaaSGettingRemoteServerData()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsClient.CON.UseCaseContract.DNS_CLIENT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL_SAAS;
            TransitionName = SkySoft.Contracts.TransitionTypes.GETTING_REMOTE_SERVER_DATA;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
//            DataContainer.RemoveCurrentRequestMetadta();
            DataContainer.AddRequestMetadata(null, null, SkySoft.Contracts.ApplicationLayerNames.DPL, null, null);
            DataContainer = await RedirectRequestToRequestHandler(DataContainer);
            DataContainer.RemoveCurrentRequestMetadta();
        }
        #endregion
    }
}
