
namespace SkySoft.Core
{
    /// <summary>
    /// Defines settings provider public members
    /// </summary>
    public interface ISettingsProvider
    {
        #region Methods
        /// <summary>
        /// Loads settings
        /// </summary>
        /// <returns>Loaded settings</returns>
        ISettings LoadConfiguration();
        #endregion
    }
}
