using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;
using SkySoft.Net.Http;

namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// SendingRequest transition request handler
    /// </summary>
    public class SendingRequest : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SendingRequest()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            UseCaseName = SkySoft.Contracts.UseCaseTypes.CONTROLLER;
            TransitionName = SkySoft.Contracts.TransitionTypes.SENDING_REQUEST;
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
            GetDnsServerData();
            CalculateDnsServerUrl();
            if (DnsServerUrlCalculated)
            {
            }

            DataContainer.RemoveCurrentRequestMetadta();

            Transceiver transceiver = new Transceiver();
            await transceiver.TransceiveDataContainer(DataContainer, DnsServerUrl, "/processrequest", 10000);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Determines DNS server URL
        /// </summary>
        void CalculateDnsServerUrl()
        {
            if (UseHttps)
            {
                DnsServerUrl = HttpsUrl;
            }
            else
            {
                DnsServerUrl = HttpUrl;
            }
        }

        /// <summary>
        /// Gets DNS server data
        /// </summary>
        void GetDnsServerData()
        {
            DnsRecordDTO? dnsRecordDTO = DataContainer.GetLastDTOInDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS + SkySoft.Contracts.DataCollectionTypes.REQUEST_SUFFIX);
            if (dnsRecordDTO == null)
            {
                throw new KeyNotFoundException("The required DNS record is not found");
            }

            string? dnsServerUrl = null;
            if (dnsRecordDTO.UseHttps)
            {
                dnsServerUrl = dnsRecordDTO.HttpsUrl;
            }
            else
            {
                dnsServerUrl = dnsRecordDTO.HttpUrl;
            }

            if (string.IsNullOrEmpty(dnsServerUrl))
            {
                throw new KeyNotFoundException("There is no DnsServerUrl setting in appsettings.json file");
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS server URL
        /// </summary>
        string? DnsServerUrl
        {
            get; set;
        }

        bool DnsServerUrlCalculated
        {
            get
            {
                return !string.IsNullOrEmpty(DnsServerUrl);
            }
        }

        /// <summary>
        /// Gets or sets HTTP URL
        /// </summary>
        string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets HTTPS URL
        /// </summary>
        string? HttpsUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether HTTPS needs to be used
        /// </summary>
        bool UseHttps
        {
            get; set;
        }
        #endregion
    }
}
