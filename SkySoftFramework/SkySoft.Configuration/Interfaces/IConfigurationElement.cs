namespace SkySoft.Configuration
{
    /// <summary>
    /// Defines configuration element public members
    /// </summary>
    public interface IConfigurationElement
    {
        #region Properties
        /// <summary>
        /// Gets or sets "description" attribute of element
        /// </summary>
        string Description
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the required "name" attribute of element
        /// </summary>
        string Name
        {
            get; set;
        }
        #endregion
    }
}
