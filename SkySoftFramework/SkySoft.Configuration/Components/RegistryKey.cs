namespace SkySoft.Configuration
{
    /// <summary>
    /// Holds registry key configuration
    /// </summary>
    public class RegistryKey
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets registry values
        /// </summary>
        public RegistryValues? RegistryValues
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether data can be overridden by data from source key during merge with source key
        /// True means data cannot be overridden, False means data can be overridden (default behavior)
        /// </summary>
        public bool Sealed
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets state
        /// </summary>
        public string? State
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets use case engine name
        /// </summary>
        public string? UseCaseEngineName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets use case name
        /// </summary>
        public string? UseCaseName
        {
            get; set;
        }
        #endregion
    }
}
