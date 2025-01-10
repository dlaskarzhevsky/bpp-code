using Microsoft.AspNetCore.Mvc;

using SkySoft.Communication;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;
using SkySoft.APIHost.INT;

namespace SkySoft.APIHost.DPL
{
    [ApiController]
    public class ReceiverController : ControllerBase, IReceiverController
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="applicationConfigurator">Application configurator</param>
        /// <param name="operatingSystem">Operating system</param>
        public ReceiverController(IAPIHostInitializer applicationConfigurator, IOS operatingSystem)
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
