using SkySoft.Contracts;

namespace SkySoft.ICommunication
{
    /// <summary>
    /// Defines request metadata data trasfer objet functionality
    /// </summary>
    public interface IExceptionDTO : IDataTransferObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets application layer name
        /// </summary>
        string? ApplicationLayerName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets application layer URL
        /// </summary>
        string? ApplicationLayerUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets exception
        /// </summary>
        Exception? Exception
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets message
        /// </summary>
        string? Message
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets message type
        /// </summary>
        MessageType MessageType
        {
            get; set;
        }
        #endregion
    }
}
