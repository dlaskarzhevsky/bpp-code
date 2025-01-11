using SkySoft.IBPPApplication;

namespace SkySoft.APIHost.CFG
{
    /// <summary>
    /// Provides receiver controller starter functionality
    /// </summary>
    public class ReceiverControllerStarter
    {
        #region Static Methods
        /// <summary>
        /// Starts receiver controller
        /// </summary>
        /// <param name="webApplication">Web application</param>
        public static void Start(WebApplication webApplication)
        {
            webApplication.Services.GetRequiredService<IReceiverController>();
        }
        #endregion
    }
}
