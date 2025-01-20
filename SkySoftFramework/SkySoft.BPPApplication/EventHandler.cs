using SkySoft.IBPPApplication;
using SkySoft.ICommunication;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Provides event handler functionality
    /// </summary>
    public class EventHandler : RequestHandler
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
            InitializeComponent();
            ValidateComponent();
            if (ComponentIsValid)
            {
                await HandleEventAsync();
                HandleEvent();
                FinalizeComponent();
            }

            return DataContainer;
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
        /// Handles event
        /// </summary>
        protected virtual void HandleEvent()
        {
        }

        /// <summary>
        /// Handles event aynchronously
        /// </summary>
        protected virtual async Task HandleEventAsync()
        {
            await Task.Delay(0);
        }
        #endregion
    }
}
