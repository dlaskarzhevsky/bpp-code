namespace SkySoft.IBPPApplication
{
    /// <summary>
    /// Defines driver functionality
    /// </summary>
    public interface IDriver
    {
        #region Properties
        /// <summary>
        /// Gets application layer name
        /// </summary>
        string? ApplicationLayerName
        {
            get;
        }

        /// <summary>
        /// Gets domain name
        /// </summary>
        string? DomainName
        {
            get;
        }

        /// <summary>
        /// Gets or sets controller type
        /// </summary>
        string ControllerType
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets state name
        /// </summary>
        string? StateName
        {
            get;
        }

        /// <summary>
        /// Gets transition name
        /// </summary>
        string? TransitionName
        {
            get;
        }

        /// <summary>
        /// Gets application layer name
        /// </summary>
        string? UseCaseName
        {
            get;
        }
        #endregion
    }
}
