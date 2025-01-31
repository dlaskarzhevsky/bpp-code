using SkySoft.Communication;
using SkySoft.ICommunication;
using SkySoft.ILogging;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Logs messages
    /// </summary>
    internal class LogMessages
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="logger">Logger instance</param>
        public static void Execute(IDataContainer dataContainer, ILogger logger)
        {
            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            if (exceptionDataCollection != null)
            {
                for (int i = 0; i < exceptionDataCollection.Count; i++)
                {
                    ExceptionDTO exceptionDTO = exceptionDataCollection[i];
                    if (exceptionDTO.Exception == null)
                    {
                        if (!string.IsNullOrEmpty(exceptionDTO.Message))
                        {
                            logger.Log(exceptionDTO.MessageType, exceptionDTO.Message);
                        }
                    }
                    else
                    {
                        logger.Log(exceptionDTO.Exception);
                    }
                }
            }
        }
        #endregion
    }
}
