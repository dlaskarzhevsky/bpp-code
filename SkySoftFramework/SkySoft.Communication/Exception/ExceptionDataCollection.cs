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
        /// Adds error message
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <param name="errorMessage">Error message</param>
        public static void AddErrorMessage(IDataContainer dataContainer, string? errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                return;
            }

            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            IExceptionDTO exceptionDTO = dataContainer.GetNewDTO<ExceptionDTO>(exceptionDataCollection!);
            exceptionDTO.Message = errorMessage;
            exceptionDTO.MessageType = MessageType.Error;
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
        /// Gets error message
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Exception instance</returns>
        public static string? GetErrorMessage(IDataContainer dataContainer)
        {
            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            if (exceptionDataCollection == null || exceptionDataCollection.Count == 0)
            {
                return default!;
            }

            ExceptionDTO exceptionDTO = exceptionDataCollection[exceptionDataCollection.Count - 1];
            if (!string.IsNullOrEmpty(exceptionDTO.Message) && exceptionDTO.MessageType == MessageType.Error)
            {
                return exceptionDTO.Message; 
            }

            return default!;
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
