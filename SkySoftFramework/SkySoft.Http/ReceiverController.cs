using Microsoft.AspNetCore.Mvc;

using SkySoft.Communication;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.Http
{
    [ApiController]
    public class ReceiverController : ControllerBase, IReceiverController
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="operatingSystem">Operating system</param>
        public ReceiverController(IOS operatingSystem)
        {
            OperatingSystem = operatingSystem;
            ApplicationInitialized = OperatingSystem.ApplicationsInitialized;
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
                OperatingSystem.LogMessages(dataContainer);

                // Serialize data container
                string serializedDataContainer = DataContainer.Serialize(dataContainer);
                return Ok(serializedDataContainer);
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets flag indicating whether application initialized
        /// IReceiverController interface implementation
        /// </summary>
        public bool ApplicationInitialized
        {
            get;
        }
        #endregion

        #region Private Properties
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
