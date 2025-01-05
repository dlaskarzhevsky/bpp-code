using SkySoft.ICommunication;

namespace BPP.Person.DTI
{
    /// <summary>
    /// Defines person data transfer object functionality
    /// </summary>
    public interface IPersonDTO : IDataTransferObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets first name
        /// </summary>
        string? FirstName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        string? LastName
        {
            get;set;
        }

        /// <summary>
        /// Gets or sets location
        /// </summary>
        string? Location
        {
            get; set;
        }
        #endregion
    }
}
