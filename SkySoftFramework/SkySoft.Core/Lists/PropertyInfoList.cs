using System;
using System.Collections.Generic;
using System.Reflection;

namespace SkySoft.Core
{
    /// <summary>
    /// Provides PropertyInfo list functionality
    /// </summary>
    public class PropertyInfoList : List<PropertyInfo>, IDisposable
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="sourceType">Source type (required)</param>
        /// <param name="listOfSelectedFieldNames">List of selected field names</param>
        public PropertyInfoList(Type sourceType, DistinctList<string> listOfSelectedFieldNames = null)
        {
            InitializeComponent(sourceType, listOfSelectedFieldNames);
            if (ListOfPropertyNamesSpecified)
            {
                AddPropertyInfoRangeUsingListOfPropertyNames();
            }
            else
            {
                AddPropertyInfoRangeUsingAllPropertyNames();
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets list of not found properties
        /// </summary>
        public List<string> NotFoundProperties
        {
            get; private set;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or set list of property names
        /// </summary>
        DistinctList<string> ListOfPropertyNames
        {
            get; set;
        }

        /// <summary>
        /// Gets flag indicating whether list of property names specified
        /// </summary>
        bool ListOfPropertyNamesSpecified
        {
            get
            {
                return ListOfPropertyNames != null && ListOfPropertyNames.Count > 0;
            }
        }

        /// <summary>
        /// Gets or sets source type
        /// </summary>
        Type SourceType
        {
            get; set;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Fills list of PropertyInfo using all property names
        /// </summary>
        void AddPropertyInfoRangeUsingAllPropertyNames()
        {
            PropertyInfo[] arrayFoPropertyInfo = SourceType.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            AddRange(arrayFoPropertyInfo);
        }

        /// <summary>
        /// Fills list of PropertyInfo using list of property names
        /// </summary>
        void AddPropertyInfoRangeUsingListOfPropertyNames()
        {
            for (int i = 0; i < ListOfPropertyNames.Count; i++)
            {
                string propertyName = ListOfPropertyNames[i];
                PropertyInfo propertyInfo = SourceType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    NotFoundProperties.Add($"Property {propertyName} wasn't found on" + $" {SourceType}");
                }
                else
                {
                    Add(propertyInfo);
                }
            }
        }

        /// <summary>
        /// Initializes component
        /// </summary>
        /// <param name="sourceType">Source type (required)</param>
        /// <param name="listOfPropertyNames">List of property names</param>
        void InitializeComponent(Type sourceType, DistinctList<string> listOfPropertyNames)
        {
            SourceType = sourceType;
            ListOfPropertyNames = listOfPropertyNames;
            NotFoundProperties = new List<string>();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        void ReleaseResources()
        {
            ListOfPropertyNames = null;
            NotFoundProperties = null;
        }
        #endregion

        #region Destructors
        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating that object is disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseResources();
            }
        }
        #endregion
    }
}
