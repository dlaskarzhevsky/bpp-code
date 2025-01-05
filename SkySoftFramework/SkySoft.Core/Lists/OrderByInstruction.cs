using System;

namespace SkySoft.Core
{
    /// <summary>
    /// Provides Order By instruction functionality
    /// </summary>
    public class OrderByInstruction
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="value">Order By instruction, for exampe: FirstName desc</param>
        public OrderByInstruction(string value)
        {
            InitializeComponent(value);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets flag indicating whether order by property name is descending
        /// </summary>
        public bool Descending
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets property name
        /// </summary>
        public string PropertyName
        {
            get; set;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Converts ORDER BY instruction to string
        /// </summary>
        /// <returns>Converted ORDER BY instruction to string</returns>
        public override string ToString()
        {
            if (Descending)
            {
                return PropertyName + " desc";
            }
            else
            {
                return PropertyName;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes component
        /// </summary>
        /// <param name="value">Order By instruction, for exampe: FirstName desc</param>
        void InitializeComponent(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            string[] orderByParts = value.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            PropertyName = orderByParts[0].Trim();
            if (orderByParts.Length > 1)
            {
                string orderBy = orderByParts[1].Trim();
                if (orderBy.ToUpperInvariant().Contains("DESC"))
                {
                    Descending = true;
                }
            }
        }
        #endregion
    }
}
