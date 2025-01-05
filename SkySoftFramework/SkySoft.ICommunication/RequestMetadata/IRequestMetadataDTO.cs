namespace SkySoft.ICommunication
{
    /// <summary>
    /// Defines request metadata data trasfer objet functionality
    /// </summary>
    public interface IRequestMetadataDTO : IDataTransferObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets application layer name
        /// </summary>
        string? ApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets domain name
        /// </summary>
        string? DomainName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets state name
        /// </summary>
        string? StateName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets transition name
        /// </summary>
        string? TransitionName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets application layer name
        /// </summary>
        string? UseCaseName
        {
            get;
            set;
        }
        #endregion
    }
}
