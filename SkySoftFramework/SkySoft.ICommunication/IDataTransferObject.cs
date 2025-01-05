namespace SkySoft.ICommunication
{
    /// <summary>
    /// Defines data trasfer objet functionality
    /// </summary>
    public interface IDataTransferObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets GUID of user who created business object
        /// </summary>
        string? CreatedByUserGuid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets name of user who created business object
        /// </summary>
        string? CreatedByUserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets date of creation
        /// </summary>
        DateTime? DateOfCreation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets date of modification
        /// </summary>
        DateTime? DateOfModification
        {
            get; set;
        }

        /// <summary>
        /// Gets global unique identifier
        /// </summary>
        Guid? Guid
        {
            get;
        }

        /// <summary>
        /// Gets or sets unique identifier
        /// </summary>
        long? Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets GUID of user who modified business object
        /// </summary>
        string? ModifiedByUserGuid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets name of user who modified business object
        /// </summary>
        string? ModifiedByUserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets string representation
        /// </summary>
        string? StringRepresentation
        {
            get;
            set;
        }
        #endregion
    }
}
