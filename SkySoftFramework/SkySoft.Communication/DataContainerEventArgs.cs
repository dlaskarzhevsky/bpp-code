using SkySoft.ICommunication;

namespace SkySoft.Communication
{
    /// <summary>
    /// Carries ADONetDataContainer through events
    /// </summary>
    public class DataContainerEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Default construcotr
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        public DataContainerEventArgs(IDataContainer? dataContainer = null)
        {
            DataContainer = dataContainer;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets data container
        /// </summary>
        public IDataContainer? DataContainer
        {
            get; set;
        }
        #endregion
    }
}
