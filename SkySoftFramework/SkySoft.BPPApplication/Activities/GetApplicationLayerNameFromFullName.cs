namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Gets application layer name from full name
    /// </summary>
    public class GetApplicationLayerNameFromFullName
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="applicationLayerFullName">Application layer full name</param>
        /// <returns>Application layer name</returns>
        public static string Execute(string applicationLayerFullName)
        {
            string[] applicationLayerFullNameParts = applicationLayerFullName.Split('_');
            string applicationLayerName = applicationLayerFullNameParts[2];

            return applicationLayerName;
        }
        #endregion
    }
}
