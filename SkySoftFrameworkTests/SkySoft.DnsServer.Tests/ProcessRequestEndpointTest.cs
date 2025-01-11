using SkySoft.Communication;
using SkySoft.DnsRecord.DTI;
using SkySoft.DnsRecord.DTO;
using SkySoft.ICommunication;
using SkySoft.Net.Http;

namespace SkySoft.DnsServer.Tests
{
    /// <summary>
    /// Tests "processequest" endpoint
    /// </summary>
    [TestClass]
    public sealed class ProcessRequestEndpointTest
    {
        #region Public Methods
        /// <summary>
        /// Transceives test data
        /// </summary>
        /// <returns>Task instance</returns>
        [TestMethod]
        public async Task TransceiveTestData()
        {
            IDataContainer requestDataContainer = InitializeRequestDataContainer();
            Transceiver transceiver = new Transceiver();
            requestDataContainer.AddRequestMetadata(SkySoft.Contracts.ApplicationLayerNames.DPL, SkySoft.Contracts.DomainNames.SKYSOFT, SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER, SkySoft.DnsServer.CON.StateTypes.INITIAL, SkySoft.DnsServer.CON.TransitionTypes.SEARCHING);
            IDataContainer? responseDataContainer = await transceiver.TransceiveDataContainer(requestDataContainer, "https://localhost:7201", "/processrequest", 10000);
            if (responseDataContainer == null)
            {
                Assert.Fail("Response is null");
            }

            IDataCollection<DnsRecordDTO>? dnsRecordDTODataCollection = responseDataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER + SkySoft.DnsServer.CON.DataCollectionTypes.SEARCH_RESPONSE);
            Assert.IsNotNull(dnsRecordDTODataCollection);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes request data container
        /// </summary>
        /// <returns>Request data container</returns>
        IDataContainer InitializeRequestDataContainer()
        {
            IDataContainer requestDataContainer = DataContainer.CreateDataContainer();
            requestDataContainer.DomainName = SkySoft.Contracts.DomainNames.SKYSOFT;
            requestDataContainer.ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DPL;
            requestDataContainer.UseCaseName = SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER;
            requestDataContainer.StateName = SkySoft.DnsServer.CON.StateTypes.INITIAL;
            requestDataContainer.TransitionName = SkySoft.DnsServer.CON.TransitionTypes.SEARCHING;

            return requestDataContainer;
        }
        #endregion
    }
}
