using SkySoft.Contracts;
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
        /// Gets or sets application layer name
        /// IExceptionDTO interface implementation
        /// </summary>
        public string? ApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets application layer URL
        /// IExceptionDTO interface implementation
        /// </summary>
        public string? ApplicationLayerUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets exception
        /// IExceptionDTO interface implementation
        /// </summary>
        public Exception? Exception
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets message
        /// IExceptionDTO interface implementation
        /// </summary>
        public string? Message
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets message type
        /// IExceptionDTO interface implementation
        /// </summary>
        public MessageType MessageType
        {
            get; set;
        } = MessageType.NotSet;
        #endregion
    }
}
