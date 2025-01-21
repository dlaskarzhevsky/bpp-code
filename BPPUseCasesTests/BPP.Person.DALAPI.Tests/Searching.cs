using BPP.Person.CON;
using BPP.Person.DTI;
using BPP.Person.DTO;

using SkySoft.Communication;
using SkySoft.DnsRecord.DTI;
using SkySoft.DnsRecord.DTO;
using SkySoft.Http;
using SkySoft.ICommunication;

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
            AddPersonSearchCriteriaToRequestDataContainer(requestDataContainer);
            Transceiver transceiver = new Transceiver();
            IDataContainer? responseDataContainer = await transceiver.TransceiveDataContainer(requestDataContainer, "http://localhost:5078", "/processrequest", 10000);
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
            requestDataContainer.UseCaseName = UseCaseContract.PERSON;
            requestDataContainer.ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            requestDataContainer.StateName = StateTypes.INITIAL;
            requestDataContainer.TransitionName = TransitionTypes.SEARCHING;

            return requestDataContainer;
        }
        #endregion
    }
}
