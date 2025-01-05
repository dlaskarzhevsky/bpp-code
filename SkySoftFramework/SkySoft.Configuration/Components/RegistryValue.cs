namespace SkySoft.Configuration
{
    /// <summary>
    /// Holds registry value configuration
    /// </summary>
    public class RegistryValue 
    {
        #region Public Properties
        /// <summary>
        /// Gets data
        /// </summary>
        public string Data
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets data type
        /// </summary>
        public string DataType
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets flag indicating whether data can be overridden by data from source value during merge with source value
        /// True means data cannot be overridden, False means data can be overridden (default behavior)
        /// </summary>
        public bool Sealed
        {
            get; set;
        }
        #endregion
    }
}
