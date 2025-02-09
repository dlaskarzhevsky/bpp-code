namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Gets application layer full name
    /// </summary>
    public class GetApplicationLayerFullName
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="domainName">Domain name</param>
        /// <param name="useCaseName">Use case name</param>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <returns>Application layer full name</returns>
        public static string Execute(string domainName, string useCaseName, string applicationLayerName)
        {
            string applicationLayerFullNameTemplate = $"{domainName}_{useCaseName}_{applicationLayerName}";

            return applicationLayerFullNameTemplate;
        }
        #endregion
    }
}
