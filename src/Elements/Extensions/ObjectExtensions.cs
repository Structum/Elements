using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Structum.Elements.Extensions
{
    /// <summary>
    ///     Defines the Object Extensions.
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
        ///     Calculates the object's hash using Knuth's algorithm.
        /// </summary>
        /// <param name="obj">Object to hash.</param>
        /// <returns>Knuth Hash representation of the object.</returns>
        public static ulong CalculateKnuthHash(this object obj)
        {
            string json = fastJSON.JSON.ToJSON(obj);
            return StringExtension.CalculateKnuthHash(json);
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
    }
}