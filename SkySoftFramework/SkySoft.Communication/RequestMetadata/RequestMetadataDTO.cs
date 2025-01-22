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
        /// Gets or sets application layer full name
        /// IRequestMetadataDTO interface implementation
        /// </summary>
        public string? ApplicationLayerFullName
        {
            get
            {
                return CompileApplicationLayerFullName();
            }
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

        #region Private Methods
        /// <summary>
        /// Compiles application layer full name
        /// </summary>
        /// <returns>Compiled application layer full name</returns>
        string CompileApplicationLayerFullName()
        {
            string applicationLayerFullName = string.Empty;
            string applicationLayerName = string.Empty;
            string domainName = string.Empty;
            string useCaseName = string.Empty;

            if (!string.IsNullOrEmpty(DomainName))
            {
                domainName = DomainName;
            }

            if (!string.IsNullOrEmpty(UseCaseName))
            {
                useCaseName = UseCaseName;
            }

            if (!string.IsNullOrEmpty(ApplicationLayerName))
            {
                applicationLayerName = ApplicationLayerName;
            }

            applicationLayerFullName = $"{domainName}_{useCaseName}_{applicationLayerName}";
            return applicationLayerFullName;
        }
        #endregion
    }
}
