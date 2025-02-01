using SkySoft.Contracts;
using SkySoft.DnsRecord.DTO;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Provides host data validator class
    /// </summary>
    public class DnsRecordValidator : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dnsRecord">DNS record</param>
        /// <param name="requestHandler">Request handler</param>
        /// <param name="logValidationResult">Flag indicating whether validation result needs to be logged</param>
        /// <param name="messageHeader">Message header</param>
        public DnsRecordValidator(DnsRecordDTO dnsRecord, IRequestHandler requestHandler, bool logValidationResult, string? messageHeader = null)
        {
            DnsRecordDTO = dnsRecord;
            RequestHandler = requestHandler;
            LogValidationResult = logValidationResult;
            MessageHeader = messageHeader;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            ValidateHostApplicationLayerName();
            if (DnsRecordDataValid)
            {
                ValidateUrl();
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public override void ReleaseResources()
        {
            RequestHandler = default!;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validates host application layer name
        /// </summary>
        void ValidateHostApplicationLayerName()
        {
            if (string.IsNullOrEmpty(DnsRecordDTO.ApplicationLayerName))
            {
                if (LogValidationResult)
                {
                    LogMessage($"{MessageHeader}DNS data does not have required ApplicationLayerName entry", MessageType.Error);
                }

                DnsRecordDataValid = false;
            }
        }

        /// <summary>
        /// Validates URL
        /// </summary>
        void ValidateUrl()
        {
            if (string.IsNullOrEmpty(DnsRecordDTO.Url))
            {
                if (LogValidationResult)
                {
                    LogMessage($"{MessageHeader}DNS data file does not have required URL entry for " + DnsRecordDTO.ApplicationLayerName, MessageType.Warning);
                }

                DnsRecordDataValid = false;
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets flag indicating whether host data valid
        /// </summary>
        public bool DnsRecordDataValid
        {
            get; set;
        } = true;
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets DNS record
        /// </summary>
        DnsRecordDTO DnsRecordDTO
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether validation result needs to be logged
        /// </summary>
        bool LogValidationResult
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets message header
        /// </summary>
        string? MessageHeader
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets request handler
        /// </summary>
        IRequestHandler RequestHandler
        {
            get; set;
        }
        #endregion
    }
}
