using BPP.Person.DTI;

using SkySoft.Communication;

namespace BPP.Person.DTO
{
    /// <summary>
    /// Provides person data transfer object functionality
    /// </summary>
    public class PersonDTO : DataTransferObject, IPersonDTO
    {
        #region Properties
        /// <summary>
        /// Gets or sets first name
        /// IPerson interface implementation
        /// </summary>
        public string? FirstName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets last name
        /// IPerson interface implementation
        /// </summary>
        public string? LastName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets location
        /// IPerson interface implementation
        /// </summary>
        public string? Location
        {
            get; set;
        }
        #endregion
    }
}
