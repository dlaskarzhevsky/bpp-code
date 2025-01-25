using SkySoft.Communication;
using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

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
        /// <param name="operatingSystem">Operating system</param>
        public static void Execute(IDataContainer dataContainer, IOS operatingSystem)
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
                            operatingSystem.LogMessage(exceptionDTO.Message, MessageTypeToLogLevelMapper.Map(exceptionDTO.MessageType));
                        }
                    }
                    else
                    {
                        operatingSystem.LogException(exceptionDTO.Exception);
                    }
                }
            }
        }
        #endregion
    }
}
