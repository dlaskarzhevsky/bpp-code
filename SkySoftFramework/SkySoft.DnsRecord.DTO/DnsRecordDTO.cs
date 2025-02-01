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
        /// Gets or set uniform resource locator
        /// IDnsRecordDTO interface implementation
        /// </summary>
        public string? Url
        {
            get; set;
        }
        #endregion
    }
}
