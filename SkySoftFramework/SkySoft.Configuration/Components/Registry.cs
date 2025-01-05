namespace SkySoft.Configuration
{
    /// <summary>
    /// Holds registry configuration
    /// </summary>
    public class Registry
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets name
        /// IRegistry intereface implementation
        /// </summary>
        public string Name
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets method name
        /// IRegistry intereface implementation
        /// </summary>
        public RegistryKeys? RegistryKeys
        {
            get; set;
        }
        #endregion
    }
}
