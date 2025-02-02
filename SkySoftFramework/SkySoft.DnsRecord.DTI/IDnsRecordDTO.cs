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
        /// Gets or sets application layer full name
        /// </summary>
        string? ApplicationLayerFullName
        {
            get; set;
        }

        /// <summary>
        /// Gets or set uniform resource locator
        /// </summary>
        string? Url
        {
            get; set;
        }
        #endregion
    }
}
