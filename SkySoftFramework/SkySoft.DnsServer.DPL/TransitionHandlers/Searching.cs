using SkySoft.DnsRecord.DTO;
using SkySoft.DnsClientServerComponents;

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
            CopyDnsDataFromCachedRecordIntoRecordFromRequest();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            DnsRecordFromRequest = null;
            FoundCachedDnsRecordDTO = null;
            ListOfDnsRecords = null;
            base.ReleaseResources();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Copies DNS data from cached record into record from request
        /// </summary>
        void CopyDnsDataFromCachedRecordIntoRecordFromRequest()
        {
            CopyDnsRecordData.Execute(FoundCachedDnsRecordDTO!, DnsRecordFromRequest!, false);
        }

        /// <summary>
        /// Finds data of DNS record from request by application layer name
        /// </summary>
        void FindDataOfDnsRecordFromRequestByApplicationLayerName()
        {
            FoundCachedDnsRecordDTO = FindDnsRecordInListByApplicationLayerName.Execute(ListOfDnsRecords!, DnsRecordFromRequest!.ApplicationLayerName!);
        }

        /// <summary>
        /// Gets DNS record from request
        /// </summary>
        void GetDnsRecordFromRequest()
        {
            DnsRecordFromRequest = GetLastDnsRecordFromDataContainer.Execute(DataContainer);
        }

        /// <summary>
        /// Gets list of DNS records from cache
        /// </summary>
        void GetListOfDnsRecordsFromCache()
        {
            ListOfDnsRecords = SkySoft.DnsClientServerComponents.GetListOfDnsRecordsFromCache.Execute(OperatingSystem);
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
        /// Gets or sets found cached DNS Record
        /// </summary>
        DnsRecordDTO? FoundCachedDnsRecordDTO
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
