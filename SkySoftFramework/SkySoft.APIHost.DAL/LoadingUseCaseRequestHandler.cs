using System.Reflection;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using SkySoft.Communication;
using SkySoft.Contracts;
using SkySoft.Core;
using SkySoft.DnsServer.DTO;
using SkySoft.ICommunication;

using static System.TimeZoneInfo;

namespace SkySoft.APIHost.DAL
{
    public class LoadingUseCaseRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingUseCaseRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            UseCaseName = SkySoft.APIHost.CON.UseCaseContract.API_HOST;
            TransitionName = SkySoft.APIHost.CON.TransitionTypes.LOADING_USE_CASE;
        }
        #endregion


        #region Overridden Methods
        /// <summary>
        /// Initializes component
        /// </summary>
        protected override void ValidateComponent()
        {
            if (ApplicationConfiguration == null)
            {
                throw new ConfigurationException("Configuration is not loaded");
            }
        }

        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override async Task HandleRequest()
        {
            await Task.Delay(0);
            GetHostData();
            AddHostDataToRequest();
            await RegisterHostDataWithDnsServer();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds host data to request
        /// </summary>
        void AddHostDataToRequest()
        {
            IDataCollection<DnsRecordDTO>? dnsRecordDTODataCollection = DataContainer!.GetDataColletion<DnsRecordDTO>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS + SkySoft.DnsServer.CON.DataCollectionTypes.REQUEST_SUFFIX);
            DnsRecordDTO dnsRecordDTO = DataContainer.GetNewDTO<DnsRecordDTO>(dnsRecordDTODataCollection);
            dnsRecordDTO.HttpsUrl = HttpsUrl;
            dnsRecordDTO.HttpUrl = HttpUrl;
            dnsRecordDTO.ApplicationLayerName = HostApplicationLayerName;
        }

        /// <summary>
        /// Gets host data
        /// </summary>
        void GetHostData()
        {
            HttpUrl = ApplicationConfiguration!.GetValue<string>("Kestrel:Endpoints:Http:Url");
            HttpsUrl = ApplicationConfiguration!.GetValue<string>("Kestrel:Endpoints:Https:Url");
            HostApplicationLayerName = ApplicationConfiguration!.GetValue<string>("ApplicationLayerName");
        }

        /// <summary>
        /// Registers host data with DNS server
        /// </summary>
        async Task RegisterHostDataWithDnsServer()
        {
            DataContainer!.AddRequestMetadata(
            SkySoft.Contracts.ApplicationLayerNames.DAL,
            SkySoft.Contracts.DomainNames.SKYSOFT,
                SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER,
                SkySoft.DnsServer.CON.StateTypes.INITIAL,
                SkySoft.DnsServer.CON.TransitionTypes.REGISTERING_HOST);

        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets host application layer name
        /// </summary>
        string? HostApplicationLayerName
        {
            get; set;
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
        #endregion
    }
}
