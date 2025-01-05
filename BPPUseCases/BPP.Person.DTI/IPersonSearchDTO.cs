using SkySoft.ICommunication;

namespace BPP.Person.DTI
{
    /// <summary>
    /// Defines person search data transfer object functionality
    /// </summary>
    public interface IPersonSearchDTO : IDataTransferObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets data access logic layer location
        /// </summary>
        string DataAccessLogicLayerLocation
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets data processing logic layer location
        /// </summary>
        string DataProcessingLogicLayerLocation
        {
            get; set;
        }

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
        #endregion
    }
}
