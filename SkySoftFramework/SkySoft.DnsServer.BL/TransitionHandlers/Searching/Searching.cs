namespace SkySoft.DnsServer.BL
{
    /// <summary>
    /// Searching transition request handler
    /// </summary>
    public partial class Searching : SaaSBusinessLogicTransitionHandler
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Searching()
        {
            TransitionName = SkySoft.DnsServer.CON.TransitionTypes.SEARCHING;
        }
        #endregion
    }
}
