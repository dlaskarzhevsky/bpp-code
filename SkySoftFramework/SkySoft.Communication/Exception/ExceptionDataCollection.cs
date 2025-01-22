using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides exception data collection functionality
    /// </summary>
    static class ExceptionDataCollection
    {
        #region Public Methods
        /// <summary>
        /// Adds message
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="message">Message text</param>
        /// <param name="messageType">Message type</param>
        public static void AddMessage(IDataContainer dataContainer, string? message, MessageType messageType)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            IExceptionDTO exceptionDTO = dataContainer.GetNewDTO<ExceptionDTO>(exceptionDataCollection!);
            exceptionDTO.Message = message;
            exceptionDTO.MessageType = messageType;
        }

        /// <summary>
        /// Adds exception
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="exception">Exception instance</param>
        public static void AddException(IDataContainer dataContainer, Exception? exception)
        {
            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            IExceptionDTO exceptionDTO = dataContainer.GetNewDTO<ExceptionDTO>(exceptionDataCollection!);
            exceptionDTO.Exception = exception;
        }

        /// <summary>
        /// Clears messages
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        public static void ClearMessages(IDataContainer dataContainer)
        {
            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            exceptionDataCollection!.Clear();
        }

        /// <summary>
        /// Gets message
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Exception instance</returns>
        public static string? GetMessage(IDataContainer dataContainer)
        {
            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            if (exceptionDataCollection == null || exceptionDataCollection.Count == 0)
            {
                return default!;
            }

            ExceptionDTO exceptionDTO = exceptionDataCollection[exceptionDataCollection.Count - 1];
            if (!string.IsNullOrEmpty(exceptionDTO.Message))
            {
                return exceptionDTO.Message; 
            }

            return default!;
        }

        /// <summary>
        /// Gets message type
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Exception instance</returns>
        public static MessageType GetMessageType(IDataContainer dataContainer)
        {
            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            if (exceptionDataCollection == null || exceptionDataCollection.Count == 0)
            {
                return default!;
            }

            ExceptionDTO exceptionDTO = exceptionDataCollection[exceptionDataCollection.Count - 1];
            return exceptionDTO.MessageType;
        }

        /// <summary>
        /// Gets exception
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Exception instance</returns>
        public static Exception? GetException(IDataContainer dataContainer)
        {
            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            if (exceptionDataCollection == null || exceptionDataCollection.Count == 0)
            {
                return default!;
            }

            ExceptionDTO exceptionDTO = exceptionDataCollection[exceptionDataCollection.Count - 1];
            return exceptionDTO.Exception;
        }
        #endregion
    }
}
