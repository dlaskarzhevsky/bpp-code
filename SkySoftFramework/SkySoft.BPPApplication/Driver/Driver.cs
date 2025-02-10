using SkySoft.IBPPApplication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides driver functionality
    /// </summary>
    public class Driver : IDriver
    {
        #region Public Properties
        /// <summary>
        /// Gets application layer name
        /// IDriver iterface implementation
        /// </summary>
        public string? ApplicationLayerName
        {
            get; protected set;
        } = default!;

        /// <summary>
        /// Gets or sets controller type
        /// IDriver iterface implementation
        /// </summary>
        public string ControllerType
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets domain name
        /// IDriver iterface implementation
        /// </summary>
        public string DomainName
        {
            get; protected set;
        } = default!;

        /// <summary>
        /// Gets state name
        /// IDriver iterface implementation
        /// </summary>
        public string? StateName
        {
            get; protected set;
        } = default!;

        /// <summary>
        /// Gets transition name
        /// IDriver iterface implementation
        /// </summary>
        public string? TransitionName
        {
            get; protected set;
        } = default!;

        /// <summary>
        /// Gets application layer name
        /// IDriver iterface implementation
        /// </summary>
        public string? UseCaseName
        {
            get; protected set;
        } = default!;
        #endregion
    }
}
