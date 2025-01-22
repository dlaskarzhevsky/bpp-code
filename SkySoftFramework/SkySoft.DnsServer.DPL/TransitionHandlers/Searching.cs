using Microsoft.Extensions.Caching.Memory;

using SkySoft.DnsServer.CON;
using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;

namespace SkySoft.DnsServer.DPL
{
    /// <summary>
    /// Searching transition request handler
    /// </summary>
    public class Searching : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Searching()
        {
            DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.SEARCHING;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        protected override void HandleRequest()
        {
            GetListOfDnsRecordsFromCache();
            GetDnsRecordFromRequest();
            FindDataOfDnsRecordFromRequestByApplicationLayerName();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            DnsRecordFromRequest = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Finds data of DNS record from request by application layer name
        /// </summary>
        void FindDataOfDnsRecordFromRequestByApplicationLayerName()
        {
            for (int i = 0; i < ListOfDnsRecords!.Count; i++)
            {
                string? registeredApplicationLayerName = ListOfDnsRecords[i].ApplicationLayerName;
                if (string.Equals(registeredApplicationLayerName, DnsRecordFromRequest!.ApplicationLayerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    DnsRecordFromRequest.HttpUrl = ListOfDnsRecords[i].HttpUrl;
                    DnsRecordFromRequest.HttpsUrl = ListOfDnsRecords[i].HttpsUrl;
                    DnsRecordFromRequest.UseHttps = ListOfDnsRecords[i].UseHttps;
                    break;
                }
            }
        }

        /// <summary>
        /// Gets DNS record from request
        /// </summary>
        void GetDnsRecordFromRequest()
        {
            DnsRecordFromRequest = DataContainer.GetLastDTOFromDataCollection<DnsRecordDTO>(SkySoft.Contracts.DataCollectionTypes.DNS_RECORDS);
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            List<DnsRecordDTO>? listOfDnsRecords;
            MemoryCache.TryGetValue<List<DnsRecordDTO>>(SkySoft.DnsServer.CON.DataCollectionTypes.DNS_RECORDS, out listOfDnsRecords);
            if (listOfDnsRecords != null)
            {
                ListOfDnsRecords = listOfDnsRecords;
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS record from request
        /// </summary>
        DnsRecordDTO? DnsRecordFromRequest
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets list of DNS records
        /// </summary>
        List<DnsRecordDTO>? ListOfDnsRecords
        {
            get; set;
        }
        #endregion
    }
}
