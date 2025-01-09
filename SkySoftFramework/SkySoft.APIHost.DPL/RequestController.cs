using Microsoft.AspNetCore.Mvc;

using SkySoft.Communication;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;
using SkySoft.APIHost.INT;
using SkySoft.Net.Http;
using SkySoft.IOperatingSystem;

namespace SkySoft.APIHost.DPL
{
    [ApiController]
    public class RequestController : ControllerBase, IRequestController
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="applicationConfigurator">Application configurator</param>
        /// <param name="operatingSystem">Operating system</param>
        public RequestController(IAPIHostInitializer applicationConfigurator, IOS operatingSystem)
        {
            OperatingSystem = operatingSystem;
            applicationConfigurator.ConfigureApiHost(operatingSystem);
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("online")]
        /// <summary>
        /// Verifies API online status
        /// </summary>
        /// <returns>Health check result</returns>
        public string VerifyApiOnlineStatus()
        {
            return "OK";
        }

        [HttpPost]
        [Route("processrequest")]
        /// <summary>
        /// Processes request
        /// </summary>
        /// <returns>Result of processed request</returns>
        public async Task<IActionResult> ProcessRequest()
        {
            string? plainText = null;
            using (var reader = new StreamReader(Request.Body))
            {
                plainText = await reader.ReadToEndAsync();
            }

            IDataContainer? dataContainer = DataContainer.Deserialize(plainText);
            if (dataContainer == null)
            {
                return BadRequest();
            }
            else
            {
                dataContainer = await OperatingSystem.RedirectRequestToRequestHandler(dataContainer);

                // Serialize data container
                string serializedDataContainer = DataContainer.Serialize(dataContainer);
                return Ok(serializedDataContainer);
            }
        }

        /// <summary>
        /// Sends request to DNS server
        /// IRequestController interface implementation
        /// </summary>
        /// <param name="requestDataContainer">Request data container</param>
        /// <returns>Response data container</returns>
        [NonAction]
        public async Task<IDataContainer> SendRequestToDnsServer(IDataContainer requestDataContainer)
        {
            string? dnsServerUrl = OperatingSystem.GetValueFromApplicationConfiguration<string>("DnsServerUrl");
            if (string.IsNullOrEmpty(dnsServerUrl))
            {
                throw new KeyNotFoundException("There is no DnsServerUrl setting in appsettings.json file");
            }

            Transceiver transceiver = new Transceiver();
            IDataContainer? responseDataContainer = await transceiver.TransceiveDataContainer(requestDataContainer, dnsServerUrl, "/processrequest", 10000);
            if (responseDataContainer == null)
            {
                throw new ApplicationException("DNS eerver is not online");
            }

            return responseDataContainer;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets or sets operating system
        /// </summary>
        IOS OperatingSystem
        {
            get; set;
        }
        #endregion
    }
}
