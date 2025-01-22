using BPP.Person.CON;

using BPP.Person.DTI;
using BPP.Person.DTO;

using SkySoft.Communication;
using SkySoft.ICommunication;

namespace BPP.Person.DAL
{
    /// <summary>
    /// Provides searching request handler functionality
    /// </summary>
    public class SearchingRequestHandler : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchingRequestHandler()
        {
            DomainName = SkySoft.Contracts.DomainNames.BPP;
            UseCaseName = UseCaseContract.PERSON;
            ApplicationLayerName = SkySoft.Contracts.ApplicationLayerNames.DAL;
            StateName = StateTypes.INITIAL;
            TransitionName = TransitionTypes.SEARCHING;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            // Get person search criteria from data container
            IDataCollection<PersonSearchDTO>? personSearchDataCollection = DataContainer.GetDataColletion<PersonSearchDTO>(UseCaseContract.PERSON + DataCollectionTypes.SEARCH_REQUEST);
            if (personSearchDataCollection == null)
            {
                return;
            }

            IPersonSearchDTO personSearchCriteria = personSearchDataCollection[0];
            string? firstName = personSearchCriteria.FirstName;
            string? lastName = personSearchCriteria.LastName;

            // Using firstName and lastName search citeria, do the actual search in a database here.
            // The following is just a mocking result returning some persons with Tom first name.

            DataCollection<PersonDTO> personDataCollection = new DataCollection<PersonDTO>();
            DataContainer.AddDataCollection(UseCaseContract.PERSON + DataCollectionTypes.SEARCH_RESPONSE, personDataCollection);

            PersonDTO personDTO = new PersonDTO();
            personDTO.FirstName = "Tom";
            personDTO.LastName = "Jerry";
            personDTO.Location = $"{SkySoft.Contracts.DomainNames.BPP}_{UseCaseContract.PERSON}_{SkySoft.Contracts.ApplicationLayerNames.DAL}";
            personDataCollection.Add(personDTO);

            personDTO = new PersonDTO();
            personDTO.FirstName = "Tom";
            personDTO.LastName = "Cat";
            personDTO.Location = $"{SkySoft.Contracts.DomainNames.BPP}_{UseCaseContract.PERSON}_{SkySoft.Contracts.ApplicationLayerNames.DAL}";
            personDataCollection.Add(personDTO);
        }
        #endregion
    }
}
