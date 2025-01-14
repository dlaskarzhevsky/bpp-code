using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides event handler functionality
    /// </summary>
    public class EventHandler : RequestHandler, IEventHandler
    {
        #region Overriden Methods
        /// <summary>
        /// Processes request
        /// IRequestHandler iterface implementation
        /// </summary>
        /// <param name="dataContainer">Data container</param>
        /// <returns>Data container</returns>
        public override async Task<IDataContainer> ProcessRequestAsync(IDataContainer dataContainer)
        {
            DataContainer = dataContainer;
            DataContainer.RemoveCurrentRequestMetadta();
            InitializeComponent();
            ValidateComponent();
            if (ComponentIsValid)
            {
                await HandleEvent();
                FinalizeComponent();
            }

            return dataContainer;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets event name
        /// </summary>
        public string? EventName
        {
            get
            {
                return TransitionName;
            }
            set
            {
                TransitionName = value;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected virtual async Task HandleEvent()
        {
            await Task.Delay(0);
        }

        /// <summary>
        /// Handles request
        /// </summary>
        private new async Task HandleRequest()
        {
            await Task.Delay(0);
        }
        #endregion
    }
}
