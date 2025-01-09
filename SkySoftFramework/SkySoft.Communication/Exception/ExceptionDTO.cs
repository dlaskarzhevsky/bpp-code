using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Provides request metadata data trasfer objet functionality
    /// </summary>
    public class ExceptionDTO : DataTransferObject, IExceptionDTO
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets exception
        /// IRequestMetadataDTO interface implementation
        /// </summary>
        public Exception? Exception
        {
            get;
            set;
        }
        #endregion
    }
}
