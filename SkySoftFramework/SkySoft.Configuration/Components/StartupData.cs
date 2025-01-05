using System;
using System.IO;

namespace SkySoft.Configuration
{
    /// <summary>
    /// Defines startup data
    /// </summary>
    public class StartupData
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="executingAssemblyLocation">Executing assembly location (required)</param>
        public StartupData(string executingAssemblyLocation)
        {
            InitializeComponent(executingAssemblyLocation);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets executing assembly location
        /// </summary>
        public string ExecutingAssemblyLocation
        {
            get; private set;
        } = default!;

        /// <summary>
        /// Gets or sets service name
        /// </summary>
        public string ServiceName
        {
            get; set;
        } = default!;

        /// <summary>
        /// Gets or sets service name data
        /// </summary>
        public string[]? ServiceNameData
        {
            get; set;
        }
        #endregion

        #region Provate Methods
        /// <summary>
        /// Calculates service name data
        /// </summary>
        void CalculateServiceNameData()
        {
            ServiceName = Path.GetFileNameWithoutExtension(ExecutingAssemblyLocation);
            ServiceNameData = new string[2];
            ServiceNameData[0] = ServiceName.Substring(0, 2);
            ServiceNameData[1] = ServiceName.Substring(2);

            switch (ServiceNameData[0])
            {
                case "S0":
                case "S1":
                case "S2":
                case "S3":
                case "S4":
                case "S5":
                case "S6":
                case "S7":
                case "S8":
                case "S9":
                    break;
                default:
                    ServiceNameData[0] = default!;
                    ServiceNameData[1] = ServiceName;
                    break;
            }
        }

        /// <summary>
        /// Initializes component
        /// </summary>
        /// <param name="executingAssemblyLocation"></param>
        void InitializeComponent(string executingAssemblyLocation)
        {
            if (string.IsNullOrEmpty(executingAssemblyLocation))
            {
                throw new ArgumentNullException(nameof(executingAssemblyLocation));
            }

            ExecutingAssemblyLocation = executingAssemblyLocation;
            CalculateServiceNameData();
        }
        #endregion
    }
}
