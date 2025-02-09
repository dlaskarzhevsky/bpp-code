namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Normilizes application layer name
    /// </summary>
    public class NormilizeApplicationLayerlName
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="applicationLayerName">Application layer name</param>
        /// <returns>Normilizes application layer name</returns>
        public static string Execute(string applicationLayerFullName)
        {
            if (applicationLayerFullName.StartsWith(SkySoft.Contracts.ApplicationLayerNames.BL))
            {
                return SkySoft.Contracts.ApplicationLayerNames.BL;
            }

            if (applicationLayerFullName.StartsWith(SkySoft.Contracts.ApplicationLayerNames.DAL))
            {
                return SkySoft.Contracts.ApplicationLayerNames.DAL;
            }

            if (applicationLayerFullName.StartsWith(SkySoft.Contracts.ApplicationLayerNames.DPL))
            {
                return SkySoft.Contracts.ApplicationLayerNames.DPL;
            }

            return applicationLayerFullName;
        }
        #endregion
    }
}
