using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EduSafe.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates a specified number of characters from the end of a given string.
        /// </summary>
        public static string TruncateCharactersFromEnd(this string stringToTruncate, int numberOfCharactersToTruncate)
        {
            // Reveal the new string length after truncation
            var newLength = stringToTruncate.Length - numberOfCharactersToTruncate;

            // If the newLength is not less than the orginal length, return the same string
            // Otherwise, truncate the string as indicated
            return stringToTruncate.Length > newLength ? stringToTruncate.Substring(0, newLength) : stringToTruncate;
        }

        /// <summary>
        /// Truncates a string down to a specific number of characters, truncating from right to left.
        /// </summary>
        public static string TruncateToSpecificLength(this string stringToTruncate, int truncatedLengthInNumberOfCharacters)
        {
            // Reveal the new string length after truncation
            var newLength = truncatedLengthInNumberOfCharacters;

            // If the newLength is not less than the orginal length, return the same string
            // Otherwise, truncate the string as indicated
            return stringToTruncate.Length > newLength ? stringToTruncate.Substring(0, newLength) : stringToTruncate;
        }

        /// <summary>
        /// Truncate everything in a string past a specified phrase, including the phrase.
        /// </summary>
        public static string TruncateAfterPhrase(this string stringToTruncate, string phrase)
        {
            // Find the index of the phrase using ordinal sorting rules, if nothing is found -1 is returned
            var startIndexOfPhrase = stringToTruncate.IndexOf(phrase, StringComparison.Ordinal);

            // If nothing was found, return the original string
            return startIndexOfPhrase != -1 ? stringToTruncate.Substring(0, startIndexOfPhrase) : stringToTruncate;
        }

        /// <summary>
        /// Checks whether or not a string constains any of an array of specified sub-strings.
        /// </summary>
        public static bool ContainsAny(this string stringToCheck, params string[] arrayOfSubStrings)
        {
            return arrayOfSubStrings.Any(stringToCheck.Contains);
        }

        /// <summary>
        /// Checks whether or not a string constains any of an array of specified sub-strings.
        /// </summary>
        public static bool ContainsAny(this string stringToCheck, List<string> listOfSubStrings)
        {
            return listOfSubStrings.Any(p => p.Contains(stringToCheck));
        }

        /// <summary>
        /// Converts a string to title casing (e.g. "this represents a string" to "This Represents a String").
        /// </summary>
        public static string ToTitleCase(this string stringToConvert)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(stringToConvert.ToLower());
        }

        /// <summary>
        /// Converts a string to a target Enum type. The generic constraint is only on struct; be sure to pass an Enum type.
        /// </summary>
        public static TEnum ConvertToEnum<TEnum>(this string stringToConvert) where TEnum : struct, Enum
        {
            // Why is Enum.TryParse only constrained to be a struct? --Matthew M.
            if (Enum.TryParse(stringToConvert, out TEnum convertedEnum))
                return convertedEnum;

            return default;
        }
    }
}
