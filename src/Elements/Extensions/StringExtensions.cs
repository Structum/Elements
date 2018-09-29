using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Structum.Elements.Extensions
{
	/// <summary>
	/// 	Provides String Extension class.
	/// </summary>
	public static class StringExtension
	{
		/// <summary>
		/// 	Regex Pattern used to identify a GUID in a string.
		/// </summary>
		private static readonly Regex GuidPattern = new Regex("^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$", RegexOptions.Compiled);

		/// <summary>
		/// 	Special Character list.
		/// </summary>
		private static readonly char[] Specials = { '.', '-', '_', '!', '@', '$', '^', '*', '=', '~', '|', '+', '?' };

		/// <summary>
		/// 	Encodes the selected string to Base 64.
		/// </summary>
		/// <param name="toEncode">Text to Encode.</param>
		/// <returns>Base 64 Encoded representation of the selected text.</returns>
		public static string EncodeTo64(this string toEncode)
		{
			if(string.IsNullOrEmpty(toEncode)) {
				return string.Empty;
			}
			byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
			return Convert.ToBase64String(toEncodeAsBytes);
		}

		/// <summary>
		/// 	Decodes the selected base 64 string.
		/// </summary>
		/// <param name="toDecode">Base String to decode.</param>
		/// <returns>Decoded plain text.</returns>
		public static string DecodeFrom64(this string toDecode)
		{
			if(string.IsNullOrEmpty(toDecode)) {
				return string.Empty;
			}
			byte[] encodedDataAsBytes = Convert.FromBase64String(toDecode);
			return Encoding.ASCII.GetString(encodedDataAsBytes);
		}

		/// <summary>
		/// 	Truncates the selected string to the max number of characters.
		/// </summary>
		/// <param name="toTruncate">String to Truncate.</param>
		/// <param name="maxLength">Max Number of characters.</param>
		/// <returns>Truncated string.</returns>
		public static string Truncate(this string toTruncate, int maxLength)
		{
			if (string.IsNullOrEmpty(toTruncate)) {
				return toTruncate;
			}
			return toTruncate.Length <= maxLength ? toTruncate : toTruncate.Substring(0, maxLength);
		}

		/// <summary>
		/// 	Returns true if the text contains a GUID and nothing else.
		/// </summary>
		/// <param name="textToCheck">Text to check.</param>
		/// <returns><c>true</c> if the text contains a GUID, <c>false</c> otherwise.</returns>
		public static bool IsGuid(this string textToCheck)
		{
			return GuidPattern.IsMatch(textToCheck.Trim());
		}

		/// <summary>
		/// 	Converts the MongoDB UUID Binary Format to String.
		/// </summary>
		/// <param name="mongodbUuid">MongoDB UUID Binary Format.</param>
		/// <param name="type">Binary Format Type.</param>
		/// <returns>UUID as a String.</returns>
		public static string FromMongoDbUuidToString(this string mongodbUuid, int type = 3)
		{
			byte[] bytes = Convert.FromBase64String(mongodbUuid);
			string hex = BitConverter.ToString(bytes).ToLower().Replace("-","");

			StringBuilder builder = new StringBuilder();
			builder.Append(hex.Substring(0, 8));
			builder.Append("-");
			builder.Append(hex.Substring(8, 4));
			builder.Append("-");
			builder.Append(hex.Substring(12, 4));
			builder.Append("-");
			builder.Append(hex.Substring(16, 4));
			builder.Append("-");
			builder.Append(hex.Substring(20, 12));

			return builder.ToString();
		}

		/// <summary>
		/// 	Returns the Integer Representation of the string.
		/// </summary>
		/// <param name="self">Current String.</param>
		/// <param name="defaultValue">Value to Return if it cannot parse an int from the selected string.</param>
		/// <returns>Integer Representation of the string, returns the <c>defaultValue</c> if it can't parse the value.</returns>
		public static int ToInt(this string self, int defaultValue = 0)
		{
			if (!int.TryParse(self, out var val)) {
				val = defaultValue;
			}

			return val;
		}

		/// <summary>
		/// 	Returns the Boolean representation of the string.
		/// </summary>
		/// <param name="self">Current String.</param>
		/// <param name="defaultValue">Value to Return if it cannot parse an boolean from the selected string.</param>
		/// <returns>Boolean Representation of the string, returns the <c>defaultValue</c> if it can't parse the value.</returns>
		public static bool ToBool(this string self, bool defaultValue = false)
		{
			if (string.IsNullOrEmpty(self)) {
				return defaultValue;
			}

			return bool.TryParse(self, out var val) && val;
		}

		/// <summary>
		/// 	Returns the Array representation of the string using the separator value to separate array items.
		/// </summary>
		/// <param name="self">Current String.</param>
		/// <param name="separator">Array Separator.</param>
		/// <param name="defaultValue">Value to Return if it cannot parse an array from the selected string.</param>
		/// <returns>Array representation of the string, returns <c>defaultValue</c> if it can't parse the value.</returns>
		public static string[] ToArray(this string self, char separator = ';', string[] defaultValue = null)
		{
			return string.IsNullOrEmpty(self) ? defaultValue : self.Split(separator);
		}

		/// <summary>
		/// 	Returns <c>true</c> if the string contains numbers, <c>false</c> otherwise.
		/// </summary>
		/// <param name="self">Current String.</param>
		/// <returns><c>true</c> if the string contains numbers, <c>false</c> otherwise.</returns>
		public static bool ContainsNumbers(this string self)
		{
			return self.Any(char.IsDigit);
		}

		/// <summary>
		/// 	Return <c>true</c> if the string contains special characters, <c>false</c> otherwise.
		/// </summary>
		/// <param name="self">Current String.</param>
		/// <returns><c>true</c> if the string contains special characters, <c>false</c> otherwise.</returns>
		public static bool ContainsSpecialChars(this string self)
		{
			return Specials.Any(self.Contains);
		}

		/// <summary>
		/// 	Return <c>true</c> if the string contains upper case characters, <c>false</c> otherwise.
		/// </summary>
		/// <param name="self">Current String.</param>
		/// <returns><c>true</c> if the string contains upper case characters, <c>false</c> otherwise.</returns>
		public static bool ContainsUpperCaseChars(this string self)
		{
			return self.Any(char.IsUpper);
		}

		/// <summary>
		/// 	Return <c>true</c> if the string contains lower case characters, <c>false</c> otherwise.
		/// </summary>
		/// <param name="self">Current String.</param>
		/// <returns><c>true</c> if the string contains lower case characters, <c>false</c> otherwise.</returns>
		public static bool ContainsLowerCaseChars(this string self)
		{
			return self.Any(char.IsLower);
		}

		/// <summary>
		/// 	Return <c>true</c> if the string contains spaces <c>false</c> otherwise.
		/// </summary>
		/// <param name="self">Current String.</param>
		/// <returns><c>true</c> if the string contains spaces, <c>false</c> otherwise.</returns>
		public static bool ContainsSpaces(this string self)
		{
			return self.Any(char.IsWhiteSpace);
		}
	}
}

