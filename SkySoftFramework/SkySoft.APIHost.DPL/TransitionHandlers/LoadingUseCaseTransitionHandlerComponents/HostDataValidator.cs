using SkySoft.IBPPApplication;

namespace SkySoft.APIHost.DPL
{
    /// <summary>
    /// Provides host data validator class
    /// </summary>
    public class HostDataValidator : SkySoft.BPPApplication.RequestHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="hostApplicationLayerName">Host application layer name</param>
        /// <param name="httpsUrl">HTTPS URL</param>
        /// <param name="httpUrl">HTTP URL</param>
        /// <param name="useHttps">Flag indicating whether HTTPS needs to be used</param>
        /// <param name="requestHandler">Request handler</param>
        public HostDataValidator(string? hostApplicationLayerName, string? httpsUrl, string? httpUrl, bool useHttps, IRequestHandler requestHandler)
        {
            HostApplicationLayerName = hostApplicationLayerName;
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
            ValidateHostApplicationLayerName();
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
        /// Validates host application layer name
        /// </summary>
        void ValidateHostApplicationLayerName()
        {
            if (string.IsNullOrEmpty(HostApplicationLayerName))
            {
                LogErrorMessage("The appsettings.json file does not have required ApplicationLayerName entry");
                HostDataValid = false;
            }
        }

        /// <summary>
        /// Validates HTTPS URL
        /// </summary>
        void ValidateHttpsUrl()
        {
            if (string.IsNullOrEmpty(HttpsUrl) && UseHttps == true)
            {
                LogErrorMessage("The appsettings.json file does not have required Host:Endpoints:Https:Url entry");
                HostDataValid = false;
            }
        }

        /// <summary>
        /// Validates HTTP URL
        /// </summary>
        void ValidateHttpUrl()
        {
            if (string.IsNullOrEmpty(HttpUrl) && UseHttps == false)
            {
                LogErrorMessage("The appsettings.json file does not have required Host:Endpoints:Http:Url entry");
                HostDataValid = false;
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets flag indicating whether host data are valid
        /// </summary>
        public bool HostDataValid
        {
            get; set;
        } = true;
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets host application layer name
        /// </summary>
        string? HostApplicationLayerName
        {
            get; set;
        }

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
