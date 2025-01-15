using Microsoft.Extensions.Logging;

using SkySoft.IBPPApplication;

namespace SkySoft.DnsClient.DPL
{
    /// <summary>
    /// Provides DNS client data validator class
    /// </summary>
    public class DnsClientDataValidator : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="httpsUrl">HTTPS URL</param>
        /// <param name="httpUrl">HTTP URL</param>
        /// <param name="useHttps">Flag indicating whether HTTPS needs to be used</param>
        /// <param name="requestHandler">Request handler</param>
        public DnsClientDataValidator(string? httpsUrl, string? httpUrl, bool useHttps, IRequestHandler requestHandler)
        {
            HttpsUrl = httpsUrl;
            HttpUrl = httpUrl;
            UseHttps = useHttps;
            RequestHandler = requestHandler;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            ValidateHttpsUrl();
            ValidateHttpUrl();
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
        /// Validates HTTPS URL
        /// </summary>
        void ValidateHttpsUrl()
        {
            if (string.IsNullOrEmpty(HttpsUrl) && UseHttps == true)
            {
                LogErrorMessage("The appsettings.json file does not have required DnsServer:Endpoints:Https:Url entry");
                DnsClientDataValid = false;
            }
        }

        /// <summary>
        /// Validates HTTP URL
        /// </summary>
        void ValidateHttpUrl()
        {
            if (string.IsNullOrEmpty(HttpUrl) && UseHttps == false)
            {
                LogErrorMessage("The appsettings.json file does not have required DnsServer:Endpoints:Http:Url entry");
                DnsClientDataValid = false;
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets flag indicating whether DNS client data are valid
        /// </summary>
        public bool DnsClientDataValid
        {
            get; set;
        } = true;
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets HTTP URL
        /// </summary>
        string? HttpUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets HTTPS URL
        /// </summary>
        string? HttpsUrl
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

        /// <summary>
        /// Gets or sets flag indicating whether HTTPS needs to be used
        /// </summary>
        bool UseHttps
        {
            get; set;
        }
        #endregion
    }
}
