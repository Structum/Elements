using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Structum.Elements.Extensions
{
    /// <summary>
    ///     Provides Object Extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Converts the selected source to Dictionary.
        /// </summary>
        /// <param name="source">Source Object to convert.</param>
        /// <returns><c>Dictionary</c></returns>
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        /// <summary>
        ///     Converts the selected source to Dictionary.
        /// </summary>
        /// <param name="source">Source Object to convert.</param>
        /// <returns><c>Dictionary</c></returns>
        /// <typeparam name="T">Type of Dictionary Value.</typeparam>
        /// <exception cref="ArgumentNullException">source</exception>
        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source)) {
                AddPropertyToDictionary(property, source, dictionary);
            }

            return dictionary;
        }

        /// <summary>
        ///     Adds a property to the dictionary.
        /// </summary>
        /// <param name="property">Property to add.</param>
        /// <param name="source">Source Object.</param>
        /// <param name="dictionary">Dictionary instance to add the property to.</param>
        /// <typeparam name="T">Type of Value object.</typeparam>
        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, IDictionary<string, T> dictionary)
        {
            object val = property.GetValue(source);
            if (val is T) {
                dictionary.Add(property.Name, (T) val);
            }
        }

        /// <summary>
        ///     Creates a deep cloned of the selected object.
        /// </summary>
        /// <param name="obj">Object to Clone.</param>
        /// <typeparam name="T">Type of the object to clone.</typeparam>
        /// <returns>Cloned object.</returns>
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T) formatter.Deserialize(ms);
            }
        }

        /// <summary>
        ///		Converts the selected object as a Guid.
        /// </summary>
        /// <param name="dataToConvert">Data to convert.</param>
        /// <returns>Guid instance if convert was successful, Guid.Empty otherwise.</returns>
        public static Guid AsGuid(this object dataToConvert)
        {
            if(dataToConvert == null) {
                return Guid.Empty;
            }

            string dataAsString = dataToConvert.ToString();
            if(dataAsString == "System.Guid") {
                return (Guid) dataToConvert;
            }

            return !Guid.TryParse (dataAsString, out var convertedData) ? Guid.Empty : convertedData;
        }

        /// <summary>
        ///		Converts the selected object as a String.
        /// </summary>
        /// <param name="dataToConvert">Data to convert.</param>
        /// <returns>String instance if convert was successful, Guid.Empty otherwise.</returns>
        public static string AsString(this object dataToConvert)
        {
            return dataToConvert == null ? string.Empty : dataToConvert as string;
        }

        /// <summary>
        ///		Converts the selected object as a Boolean.
        /// </summary>
        /// <param name="dataToConvert">Data to convert.</param>
        /// <returns>Boolean instance if convert was successful, Guid.Empty otherwise.</returns>
        public static bool AsBoolean(this object dataToConvert)
        {
            if(dataToConvert == null) {
                return false;
            }

            string dataAsString = dataToConvert.ToString();
            if(dataAsString == "System.Boolean") {
                return (bool) dataToConvert;
            }

            return bool.TryParse(dataAsString, out var convertedData) && convertedData;
        }
    }
}