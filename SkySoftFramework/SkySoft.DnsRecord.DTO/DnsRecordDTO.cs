using SkySoft.Communication;
using SkySoft.DnsRecord.DTI;

namespace SkySoft.DnsRecord.DTO
{
    /// <summary>
    /// Provides DNS record data transfer object functionality
    /// </summary>
    public class DnsRecordDTO : DataTransferObject, IDnsRecordDTO
    {
        #region Properties
        /// <summary>
        /// Gets or sets application layer name
        /// IDnsRecordDTO interface implementation
        /// </summary>
        public string? ApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or set HTTP uniform resource locator
        /// IDnsRecordDTO interface implementation
        /// </summary>
        public string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or set HTTPS uniform resource locator
        /// IDnsRecordDTO interface implementation
        /// </summary>
        public string? HttpsUrl
        {
            get; set;
        }
        #endregion
    }
}
