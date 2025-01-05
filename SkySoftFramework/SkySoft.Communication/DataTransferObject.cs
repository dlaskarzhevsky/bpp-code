using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides data trasfer objet functionality
    /// </summary>
    public class DataTransferObject : IDataTransferObject
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets GUID of user who created business object
        /// IDTO interface implementation
        /// </summary>
        public string? CreatedByUserGuid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets name of user who created business object
        /// IDTO interface implementation
        /// </summary>
        public string? CreatedByUserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets date of creation
        /// IDTO interface implementation
        /// </summary>
        public DateTime? DateOfCreation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets date of modification
        /// IDTO interface implementation
        /// </summary>
        public DateTime? DateOfModification
        {
            get; set;
        }

        /// <summary>
        /// Gets global unique identifier
        /// IDTO interface implementation
        /// </summary>
        public Guid? Guid
        {
            get;
        }

        /// <summary>
        /// Gets or sets unique identifier
        /// IDTO interface implementation
        /// </summary>
        public long? Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets GUID of user who modified business object
        /// IDTO interface implementation
        /// </summary>
        public string? ModifiedByUserGuid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets name of user who modified business object
        /// IDTO interface implementation
        /// </summary>
        public string? ModifiedByUserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets string representation
        /// IDTO interface implementation
        /// </summary>
        public string? StringRepresentation
        {
            get;
            set;
        }
        #endregion
    }
}
