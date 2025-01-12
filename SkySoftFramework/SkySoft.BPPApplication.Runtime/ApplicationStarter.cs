using SkySoft.IBPPApplication;

namespace SkySoft.BPPApplication.Runtime
{
    /// <summary>
    /// Provides receiver controller starter functionality
    /// </summary>
    public class ApplicationStarter
    {
        #region Static Methods
        /// <summary>
        /// Starts receiver controller
        /// </summary>
        /// <param name="webApplication">Web application</param>
        public static void Start(WebApplication webApplication)
        {
            IReceiverController receiverController = webApplication.Services.GetRequiredService<IReceiverController>();
            if (receiverController.ApplicationInitialized)
            {
                webApplication.Run();
            }
            else
            {
                Console.WriteLine();
                Console.Read();
            }
        }
        #endregion
    }
}
