using SkySoft.Communication;
using SkySoft.Contracts;
using SkySoft.ICommunication;

namespace SkySoft.Http.DRV
{
    /// <summary>
    /// Provides transceiver controller functionality
    /// </summary>
    public partial class TransceiverController : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public TransceiverController()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.Contracts.UseCaseTypes.CONTROLLER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.NFA;
            TransitionName = SkySoft.Contracts.TransitionTypes.SENDING_REQUEST_TO_REMOTE_SERVER;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request aynchronously
        /// </summary>
        protected override async Task HandleRequestAsync()
        {
            await GetRemoteServerDnsRecordFromDnsClient();
            ValidateRemoteServerDnsRecord();
            if (DnsRecordOfRemoteServerValid)
            {
                await SendRequestToRemoteServer();
            }
            else
            {
                await GetDnsServerDnsRecordFromDnsClient();
                ValidateDnsServerDnsRecord();
                if (DnsRecordOfDnsServerValid)
                {
                    await SendRequestToDnsServerToSearchRemoteServerDnsRecord();
                    ValidateRemoteServerDnsRecord();
                    if (DnsRecordOfRemoteServerValid)
                    {
                        await SaveRemoteServerDnsRecordWithDnsClient();
                        await SendRequestToRemoteServer();
                    }
                }
                else
                {
                    IDataCollection<RequestMetadataDTO>? requestMetadataDataCollection = DataContainer.GetDataColletion<RequestMetadataDTO>(SkySoft.Contracts.DataCollectionTypes.REQUEST_METADATA);
                    IRequestMetadataDTO requestMetadataDTO = requestMetadataDataCollection![requestMetadataDataCollection.Count - 2];
                    DataContainer.SetMessage("DNS client does not have DNS record of application layer " + requestMetadataDTO.ApplicationLayerFullName, MessageType.Critical, OperatingSystem!.Logger.ApplicationLayerFullName, OperatingSystem.Logger.ApplicationLayerUrl);
                }
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            RemoteServerDnsRecord = null;
            RequestMetadataDTO = null;
            base.ReleaseResources();
        }
        #endregion
    }
}
