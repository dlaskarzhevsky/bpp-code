using BPP.Person.DTI;

using SkySoft.Communication;
using SkySoft.Contracts;

namespace BPP.Person.DTO
{
    /// <summary>
    /// Provides person search data transfer object functionality
    /// </summary>
    public class PersonSearchDTO : DataTransferObject, IPersonSearchDTO
    {
        #region Properties
        /// <summary>
        /// Gets or sets data access logic layer location
        /// IPerson search interface implementation
        /// </summary>
        public string DataAccessLogicLayerLocation
        {
            get; set;
        } = ApplicationLayerLocationTypes.LOCAL;

        /// <summary>
        /// Gets or sets data processing logic layer location
        /// IPerson search interface implementation
        /// </summary>
        public string DataProcessingLogicLayerLocation
        {
            get; set;
        } = ApplicationLayerLocationTypes.LOCAL;

        /// <summary>
        /// Gets or sets first name
        /// IPerson search interface implementation
        /// </summary>
        public string? FirstName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets last name
        /// IPerson search interface implementation
        /// </summary>
        public string? LastName
        {
            get; set;
        }
        #endregion
    }
}
