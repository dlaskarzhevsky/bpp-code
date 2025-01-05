using BPP.Person.CON;
using BPP.Person.DTI;
using BPP.Person.DTO;

using SkySoft.Communication;
using SkySoft.DnsServer.DTI;
using SkySoft.DnsServer.DTO;
using SkySoft.ICommunication;
using SkySoft.Net.Http;

namespace BPP.Person.DALAPI.Tests
{
    [TestClass]
    public sealed class Searching
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
            requestDataContainer.AddRequestMetadata(SkySoft.Contracts.ApplicationLayerNames.DAL, SkySoft.Contracts.DomainNames.SKYSOFT, SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER, SkySoft.DnsServer.CON.StateTypes.INITIAL, SkySoft.DnsServer.CON.TransitionTypes.SEARCHING);
            IDataContainer? responseDataContainer = await transceiver.TransceiveDataContainer(requestDataContainer, "https://localhost:7201", "/processrequest", 10000);
            if (responseDataContainer == null)
            {
                Assert.Fail("Response is null");
            }

            IDataCollection<DnsRecordDTO>? dnsRecordDTODataCollection = responseDataContainer.GetDataColletion<DnsRecordDTO>(SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER + DataCollectionTypes.SEARCH_RESPONSE);
            if (dnsRecordDTODataCollection == null)
            {
                Assert.Fail("Response is null");
            }

            IDnsRecordDTO dnsRecordDTO = dnsRecordDTODataCollection[0];
            if (string.IsNullOrEmpty(dnsRecordDTO.Url))
            {
                Assert.Fail("URL not found for application layer " + dnsRecordDTO.ApplicationLayerName);
            }

            string url = dnsRecordDTO.Url;
            responseDataContainer.RemoveDataCollection(SkySoft.DnsServer.CON.UseCaseContract.DNS_SERVER + DataCollectionTypes.SEARCH_RESPONSE);
            requestDataContainer = responseDataContainer;
            AddPersonSearchCriteriaToRequestDataContainer(requestDataContainer);
            responseDataContainer = await transceiver.TransceiveDataContainer(requestDataContainer, url, "processrequest", 10000);
            AssertResponse(responseDataContainer);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds person search criteria to request data container
        /// </summary>
        /// <param name="requestDataContainer">Request data container</param>
        void AddPersonSearchCriteriaToRequestDataContainer(IDataContainer requestDataContainer)
        {
            PersonSearchDTO personSearchDTO = new PersonSearchDTO();
            personSearchDTO.FirstName = "Tom";

            IDataCollection<PersonSearchDTO> personSearchDataCollection = new DataCollection<PersonSearchDTO>();
            personSearchDataCollection.Add(personSearchDTO);

            requestDataContainer.AddDataCollection(UseCaseContract.PERSON + DataCollectionTypes.SEARCH_REQUEST, personSearchDataCollection);
        }

        /// <summary>
        /// Asserts response
        /// </summary>
        /// <param name="responseDataContainer">Response data container</param>
        void AssertResponse(IDataContainer? responseDataContainer)
        {
            if (responseDataContainer == null)
            {
                Assert.Fail("Response is null");
            }
            else
            {
                IDataCollection<PersonDTO>? personCollectionFromResponse = responseDataContainer.GetDataColletion<PersonDTO>(UseCaseContract.PERSON + DataCollectionTypes.SEARCH_RESPONSE);
                if (personCollectionFromResponse == null)
                {
                    Assert.Fail("Response does not contain Pesron data collection");
                }
                else
                {
                    if (personCollectionFromResponse.Count != 2)
                    {
                        Assert.Fail("Pesron data collection from response does not contain data of two persons");
                    }
                    else
                    {
                        IPersonDTO person1 = personCollectionFromResponse[0];
                        IPersonDTO person2 = personCollectionFromResponse[1];
                        if (person1.FirstName != "Tom" || person1.LastName != "Jerry" || person2.FirstName != "Tom" || person2.LastName != "Cat")
                        {
                            Assert.Fail("One of the persons contain unexpected data");
                        }

                        string personExpectedLocation = $"{SkySoft.Contracts.DomainNames.BPP}_{SkySoft.Contracts.ApplicationLayerNames.DAL}_{UseCaseContract.PERSON}";
                        if (person1.Location != personExpectedLocation || person2.Location != personExpectedLocation)
                        {
                            Assert.Fail("One of the persons location is not correct");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initializes request data container
        /// </summary>
        /// <returns>Request data container</returns>
        IDataContainer InitializeRequestDataContainer()
        {
            IDataContainer requestDataContainer = DataContainer.CreateDataContainer();
            requestDataContainer.DomainName = SkySoft.Contracts.DomainNames.BPP;
            requestDataContainer.ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            requestDataContainer.UseCaseName = UseCaseContract.PERSON;
            requestDataContainer.StateName = StateTypes.INITIAL;
            requestDataContainer.TransitionName = TransitionTypes.SEARCHING;

            return requestDataContainer;
        }
        #endregion
    }
}
