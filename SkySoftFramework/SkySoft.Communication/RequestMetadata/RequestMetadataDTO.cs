using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides request metadata data trasfer objet functionality
    /// </summary>
    public class RequestMetadataDTO : DataTransferObject, IRequestMetadataDTO
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets application layer name
        /// IRequestMetadataDTO interface implementation
        /// </summary>
        public string? ApplicationLayerName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets domain name
        /// IRequestMetadataDTO interface implementation
        /// </summary>
        public string? DomainName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether request handled
        /// IRequestMetadataDTO interface implementation
        /// </summary>
        public bool RequestHandled
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets state name
        /// IRequestMetadataDTO interface implementation
        /// </summary>
        public string? StateName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets transition name
        /// IRequestMetadataDTO interface implementation
        /// </summary>
        public string? TransitionName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets application layer name
        /// IRequestMetadataDTO interface implementation
        /// </summary>
        public string? UseCaseName
        {
            get;
            set;
        }
        #endregion
    }
}
