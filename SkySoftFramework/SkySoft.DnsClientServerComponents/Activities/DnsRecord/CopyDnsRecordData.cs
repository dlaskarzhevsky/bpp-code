using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Copies DNS record data
    /// </summary>
    public static class CopyDnsRecordData
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="sourceDTO">Source data transfer object</param>
        /// <param name="targetDTO">Target data transfer object</param>
        /// <param name="copyApplicationLayerName">Flag indicating whether application layer name needs to be copied</param>
        public static void Execute(DnsRecordDTO sourceDTO, DnsRecordDTO targetDTO, bool copyApplicationLayerName)
        {
            if (copyApplicationLayerName)
            {
                if (targetDTO.ApplicationLayerFullName != sourceDTO.ApplicationLayerFullName)
                {
                    targetDTO.ApplicationLayerFullName = sourceDTO.ApplicationLayerFullName;
                }
            }

            if (targetDTO.Url != sourceDTO.Url)
            {
                targetDTO.Url = sourceDTO.Url;
            }

            if (targetDTO.DateOfCreation == null)
            {
                targetDTO.DateOfCreation = DateTime.Now;
            }

            targetDTO.DateOfModification = DateTime.Now;
        }
        #endregion
    }
}
