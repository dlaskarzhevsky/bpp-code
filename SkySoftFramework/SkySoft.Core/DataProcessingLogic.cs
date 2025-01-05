namespace SkySoft.Core
{
    /// <summary>
    /// Provides data processing logic functionality
    /// </summary>
    public class DataProcessingLogic
    {
        #region Public Method
        /// <summary>
        /// Manages data processing
        /// </summary>
        public virtual void ManageDataProcessing()
        {
            InitializeComponent();
            if (ComponentInitialized)
            {
                HandleRequest();
                FinalizeComponent();
            }

            ReleaseResources();
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets flag indicating whether component is initialized
        /// </summary>
        protected bool ComponentInitialized
        {
            get; set;
        } = true;
        #endregion

        #region Private Methods
        /// <summary>
        /// Finalizes component
        /// </summary>
        protected virtual void FinalizeComponent()
        {
        }

        /// <summary>
        /// Handles request
        /// </summary>
        protected virtual void HandleRequest()
        {
        }

        /// <summary>
        /// Initializes component
        /// </summary>
        protected virtual void InitializeComponent()
        {
        }

        /// <summary>
        /// Release resources
        /// </summary>
        protected virtual void ReleaseResources()
        {
        }
        #endregion
    }
}
