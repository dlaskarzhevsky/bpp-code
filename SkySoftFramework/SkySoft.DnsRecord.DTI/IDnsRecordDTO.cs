using SkySoft.ICommunication;

namespace SkySoft.DnsRecord.DTI
{
    /// <summary>
    /// Defines DNS record data transfer object functionality
    /// </summary>
    public interface IDnsRecordDTO : IDataTransferObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets application layer name
        /// </summary>
        string? ApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or set HTTP uniform resource locator
        /// </summary>
        string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or set HTTPS uniform resource locator
        /// </summary>
        string? HttpsUrl
        {
            get; set;
        }
        #endregion
    }
}
