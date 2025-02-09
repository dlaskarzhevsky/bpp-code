namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Normilizes application layer full name
    /// </summary>
    public class NormilizeApplicationLayerFullName
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="applicationLayerFullName">Application layer full name</param>
        /// <returns>Normilizes application layer full name</returns>
        public static string Execute(string applicationLayerFullName)
        {
            if (applicationLayerFullName.Contains('.'))
            {
                string[] applicationLayerFullNameParts = applicationLayerFullName.Split('.');
                if (applicationLayerFullNameParts.Length == 3)
                {
                    applicationLayerFullNameParts[2] = NormilizeApplicationLayerlName.Execute(applicationLayerFullNameParts[2]);
                }

                applicationLayerFullName = string.Join('_', applicationLayerFullNameParts);
            }

            return applicationLayerFullName;
        }
        #endregion
    }
}
