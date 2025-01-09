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
        /// Gets exception
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Exception instance</returns>
        public static Exception? GetException(IDataContainer dataContainer)
        {
            IDataCollection<ExceptionDTO>? exceptionDataCollection = dataContainer.GetDataColletion<ExceptionDTO>(SkySoft.Contracts.DataCollectionTypes.EXCEPTIONS);
            if (exceptionDataCollection == null)
            {
                return default!;
            }

            ExceptionDTO exceptionDTO = exceptionDataCollection[exceptionDataCollection.Count - 1];
            return exceptionDTO.Exception;
        }
        #endregion
    }
}
