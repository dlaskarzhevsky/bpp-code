using System;
using System.Reflection;

namespace SkySoft.Core
{
    /// <summary>
    /// Contains Type extention methods
    /// </summary>
    public static class TypeExtensionMethods
    {
        #region Methods
        /// <summary>
        /// Gets field value
        /// </summary>
        /// <param name="type">Type definition</param>
        /// <param name="fieldName">Field name</param>
        /// <returns></returns>
        public static T GetFieldValue<T>(this Type type, string fieldName)
        {
            FieldInfo fieldInfo = null;
            while (type != null)
            {
                fieldInfo = type.GetField(fieldName);
                if (fieldInfo != null)
                {
                    break;
                }

                type = type.BaseType;
            }

            if (fieldInfo == null)
            {
                return default(T);
            }
            else
            {
                return (T)fieldInfo.GetValue(null);
            }
        }

        /// <summary>
        /// Returns default value
        /// </summary>
        /// <param name="value">Type definition</param>
        /// <returns>Default value</returns>
        public static object GetDefaultValue(this Type value)
        {
            if (value.IsValueType)
            {
                return Activator.CreateInstance(value);
            }

            return null;
        }
        #endregion
    }
}
