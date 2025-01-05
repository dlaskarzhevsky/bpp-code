using System;
using System.Collections.Generic;
using System.Text;

namespace SkySoft.Core
{
    /// <summary>
    /// Contains List<> extention methods
    /// </summary>
    public static class GenericListExtensionMethods
    {
        #region Methods
        /// <summary>
        /// Adds business object identifier to list
        /// </summary>
        /// <param name="list">List of long</param>
        /// <param name="businessObjectIdentifier">Business object identifier</param>
        public static List<long> AddBusinessObjectIdentifier(this List<long> list, long businessObjectIdentifier)
        {
            // Validate arguments
            if (businessObjectIdentifier == 0)
            {
                throw new ArgumentNullException("businessObjectIdentifier");
            }

            list.Add(businessObjectIdentifier);
            return list;
        }

        /// <summary>
        /// Adds business object identifier to list
        /// </summary>
        /// <param name="list">List of long</param>
        /// <param name="businessObjectIdentifier">Business object identifier</param>
        public static List<long?> AddBusinessObjectIdentifier(this List<long?> list, long businessObjectIdentifier)
        {
            // Validate arguments
            if (businessObjectIdentifier == 0)
            {
                throw new ArgumentNullException("businessObjectIdentifier");
            }

            list.Add(businessObjectIdentifier);
            return list;
        }

        /// <summary>
        /// Adds unique values to list
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="list">List of values</param>
        /// <param name="value">Value for adding to list</param>
        /// <returns>List of values</returns>
        public static List<T> AddUnique<T>(this List<T> list, T value)
        {
            // Do nothing if value wasn't provided
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                return list;
            }

            // Add value to list if it desn't contains
            if (!list.Contains(value))
            {
                list.Add(value);
            }

            return list;
        }

        /// <summary>
        /// Compiles string of values separated by delimiter and surronded by brackets if needed
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="list">List of values</param>
        /// <param name="surroundByBrackets">Flag indicating whether compiled string is surrounded by brackets</param>
        /// <param name="delimiter">String for using as a delimiter between values</param>
        /// <returns>String of values separated by delimiter and surronded by brackets if needed</returns>
        public static string ToString<T>(this List<T> list, bool surroundByBrackets = false, string delimiter = ",")
        {
            // Validate argument
            if (list == null || list.Count == 0)
            {
                return null;
            }

            // Prepare string buildsr and open expression
            StringBuilder stringBuilder = new StringBuilder();
            if (surroundByBrackets)
            {
                stringBuilder.Append("(");
            }

            // Add comma-separated values to expression
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    stringBuilder.Append(list[i].ToString());
                    if (i < list.Count - 1)
                    {
                        stringBuilder.Append(delimiter);
                    }
                }
            }
            else
            {
                stringBuilder.Append(0);
            }

            // Close expression and return result
            if (surroundByBrackets)
            {
                stringBuilder.Append(")");
            }

            return stringBuilder.ToString();
        }
        #endregion
    }
}
