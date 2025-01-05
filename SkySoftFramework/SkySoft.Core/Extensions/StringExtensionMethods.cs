using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SkySoft.Core
{
    /// <summary>
    /// Contains System.String extention methods
    /// </summary>
    public static class StringExtensionMethods
    {
        #region Methods
        /// <summary>
        /// Concatenates array into string using pipeline separator
        /// </summary>
        /// <param name="data">Data for concatenation</param>
        /// <returns>Concatenated string</returns>
        public static string Concatenate(this string[] data)
        {
            return Concatenate(data, '|', 0, data.Length - 1);
        }

        /// <summary>
        /// Concatenates array into string using specified separator
        /// </summary>
        /// <param name="data">Data for concatenation</param>
        /// <param name="separator">Separator for insertion between string parts</param>
        /// <param name="startIndex">Start index</param>
        /// <param name="endIndex">End index</param>
        /// <returns>Concatenated string</returns>
        public static string Concatenate(this string[] data, char separator, int startIndex, int endIndex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = startIndex; i <= endIndex; i++)
            {
                stringBuilder.Append(data[i]);
                if (i < endIndex)
                {
                    stringBuilder.Append(separator);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Checks whether separated values contain current value
        /// </summary>
        /// <param name="value">Value for comparison</param>
        /// <param name="separatedValues">Separated values</param>
        /// <param name="separator">Separator character</param>
        /// <returns>True if separated values contains specified value, otherwise False</returns>
        public static bool ContainsIn(this string value, string separatedValues, char separator)
        {
            // Validate argument
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(separatedValues))
            {
                return false;
            }

            string[] separatedValueParts = separatedValues.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            return ContainsIn(value, separatedValueParts);
        }

        /// <summary>
        /// Checks whether list of values contains specified value
        /// </summary>
        /// <param name="value">Value for comparison</param>
        /// <param name="listOfValues">List of values</param>
        /// <returns>True if list of values contains specified value, otherwise False</returns>
        public static bool ContainsIn(this string value, string[] listOfValues)
        {
            // Validate argument
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            // Do nothing if list of excludes wasn't provided
            if (listOfValues == null)
            {
                return false;
            }

            for (int i = 0; i < listOfValues.Length; i++)
            {
                listOfValues[i] = listOfValues[i].Trim();
                if (listOfValues[i].StartsWith("*"))
                {
                    if (listOfValues[i].EndsWith("*"))
                    {
                        if (value.Contains(listOfValues[i].Substring(0, listOfValues[i].Length - 1).Substring(1)))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (value.EndsWith(listOfValues[i].Substring(1)))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (listOfValues[i].EndsWith("*"))
                    {
                        if (value.StartsWith(listOfValues[i].Substring(0, listOfValues[i].Length - 1)))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (value == listOfValues[i])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Counts specified character
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="characterForCounting">Character for counting</param>
        /// <returns>Character count</returns>
        public static int CountOf(this string value, char characterForCounting)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            int count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == characterForCounting)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Determines whether string ends with specified value
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="pattern">String for comparison (required)</param>
        /// <returns>True if string ends from specified value, otherwise False</returns>
        public static bool EndsWith(this string value, string pattern)
        {
            // Validate arguments
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException("pattern");
            }

            if (string.IsNullOrEmpty(value))
            {
                if (string.IsNullOrEmpty(pattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(pattern))
                {
                    return false;
                }
                else
                {
                    if (value.Length < pattern.Length)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = pattern.Length - 1; i >= 0; i--)
                        {
                            for (int j = value.Length - 1; j >= 0; j--)
                            {
                                if (pattern[i] != value[i])
                                {
                                    return false;
                                }
                            }
                        }

                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether string ends with specified value
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>True if string ends with number, otherwise False</returns>
        public static bool EndsWithNumber(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return char.IsNumber(value[value.Length - 1]);
        }

        /// <summary>
        /// Extracts number from string, for example: Unit 1, Suite 407
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="entranceNumber">Entrance number, for example: 2 which means 407 for above example</param>
        /// <returns>Human readable comments</returns>
        public static string ExtractNumber(this string value, int entranceNumber = 1)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            int currentEntranceNumber = 0;
            bool numericSequenceStarted = false;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsNumber(value[i]))
                {
                    if (!numericSequenceStarted)
                    {
                        numericSequenceStarted = true;
                        currentEntranceNumber++;
                    }

                    if (currentEntranceNumber == entranceNumber)
                    {
                        stringBuilder.Append(value[i]);
                    }
                }
                else
                {
                    if (numericSequenceStarted)
                    {
                        if (currentEntranceNumber == entranceNumber)
                        {
                            return stringBuilder.ToString();
                        }

                        numericSequenceStarted = false;
                    }
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Splits value into words, for example creates "abc 01 def GH ijk" from "Abc01defGHIjk"
        /// </summary>
        /// <param name="value">Value for splitting</param>
        /// <param name="preserveFirstCapital">Flag indicating whether to preserve the first capital character</param>
        /// <returns>Human readable comments</returns>
        public static string GetComments(this string value, bool preserveFirstCapital = false)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            StringBuilder constStringBuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (i > 0)
                {
                    if (
                        // Before capital letter
                        char.IsUpper(value[i]) && char.IsLetter(value[i - 1]) && !char.IsUpper(value[i - 1]) ||

                        // Before last capital letter
                        i + 1 < value.Length && char.IsUpper(value[i]) && char.IsLetter(value[i + 1]) && !char.IsUpper(value[i + 1]) ||

                        // Between digit and letter
                        char.IsDigit(value[i]) && char.IsLetter(value[i - 1]) ||

                        // Between letter and digit
                        char.IsLetter(value[i]) && char.IsDigit(value[i - 1]))
                    {
                        constStringBuilder.Append(' ');
                    }
                }

                if (i + 1 < value.Length && char.IsUpper(value[i]) && char.IsLetter(value[i + 1]) && !char.IsUpper(value[i + 1]))
                {
                    if (i == 0 && preserveFirstCapital)
                    {
                        constStringBuilder.Append(value[i]);
                    }
                    else
                    {
                        constStringBuilder.Append(char.ToLower(value[i]));
                    }
                }
                else
                {
                    constStringBuilder.Append(value[i]);
                }
            }

            return constStringBuilder.ToString(); //.ToLower();
        }

        /// <summary>
        /// Gets constant name from item name
        /// </summary>
        /// <param name="value">Value transforming into constant</param>
        /// <returns>Constant name</returns>
        public static string GetConstant(this string value)
        {
            // Validate argument
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            // Creates ABC_01_DEF_GH from Abc01defGh
            StringBuilder constStringBuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (i > 0)
                {
                    // Add underscore:
                    if (
                        // Process only letters
                        char.IsLetter(value[i]) && char.IsLetter(value[i - 1]) &&

                        // Before capital letter
                        char.IsUpper(value[i]) && !char.IsUpper(value[i - 1])
                        )
                    {
                        constStringBuilder.Append('_');
                    }
                }

                constStringBuilder.Append(value[i]);
            }

            return constStringBuilder.ToString().ToUpper();
        }

        /// <summary>
        /// Gets part of the string
        /// </summary>
        /// <param name="value">Value for getting part from</param>
        /// <param name="separator">Separator between string parts</param>
        /// <param name="partIndex">Part index</param>
        /// <returns>Specified part</returns>
        public static string GetPart(this string value, char separator, int partIndex)
        {
            // Validate argument
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            string[] parts = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            if (partIndex < 0 || partIndex >= parts.Length)
            {
                throw new ArgumentOutOfRangeException("partIndex");
            }

            return parts[partIndex];
        }

        /// <summary>
        /// Gets part of the string starting from specified position and ending by one of the specified characters
        /// </summary>
        /// <param name="value">Value for getting part from</param>
        /// <param name="startPosition">Characters at the end of the string part</param>
        /// <param name="endWith">Characters at the end of the string part</param>
        /// <param name="part">String part</param>
        /// <returns>End position</returns>
        public static string GetPart(this string value, ref int position, char[] endWith)
        {
            // Validate argument
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            StringBuilder stringBuilder = new StringBuilder();
            int endPosition = position;
            for (int i = position; i < value.Length; i++)
            {
                endPosition = i;
                if (value[i].ContainsIn(endWith))
                {
                    break;
                }

                stringBuilder.Append(value[i]);
            }

            position = endPosition + 1;
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets part of the string
        /// </summary>
        /// <param name="value">Value for getting part from</param>
        /// <param name="position">Start position</param>
        /// <param name="separator">Separator between string parts</param>
        /// <param name="partCount">Part count for returning</param>
        /// <param name="separator">Separator between string parts</param>
        /// <returns>Specified parts</returns>
        public static string[] GetParts(this string value, ref int position, int partCount, char separator)
        {
            // Creating output
            string[] parts = new string[partCount];

            // Validating arguments
            if (string.IsNullOrEmpty(value))
            {
                return parts;
            }

            // Initializing cycle
            int endPosition = 0;
            int partNumber = 0;
            StringBuilder stringBuilder = new StringBuilder();
            bool stringBuilderWasCopied = false;

            // Work with input string starting from specified position
            for (int i = position; i < value.Length; i++)
            {
                endPosition = i;
                if (value[i] == separator)
                {
                    parts[partNumber] = stringBuilder.ToString();
                    stringBuilder.Clear();

                    // Preparing to work with next part
                    partNumber++;
                    if (partNumber >= partCount)
                    {
                        // All parts have data
                        stringBuilderWasCopied = true;
                        break;
                    }
                }
                else if (value[i] == ' ')
                {
                    // Verifying whether all parts have data
                    if (partNumber == partCount - 1)
                    {
                        stringBuilderWasCopied = true;
                        parts[partNumber] = stringBuilder.ToString();

                        // All parts have data
                        break;
                    }
                    else
                    {
                        // Appending character to string builder
                        stringBuilder.Append(value[i]);
                    }
                }
                else
                {
                    // Appending character to string builder
                    stringBuilder.Append(value[i]);
                }
            }

            if (!stringBuilderWasCopied)
            {
                parts[partNumber] = stringBuilder.ToString();
            }

            position = endPosition + 1;
            return parts;
        }

        /// <summary>
        /// Gets part of the string
        /// </summary>
        /// <param name="value">Value for getting part from</param>
        /// <param name="separator">Separator between string parts</param>
        /// <param name="partIndexes">Part indexes</param>
        /// <returns>Specified parts</returns>
        public static string[] GetParts(this string value, char separator, params int[] partIndexes)
        {
            // Validate argument
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            string[] parts = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            if (partIndexes.Length < 0 || partIndexes.Length >= parts.Length)
            {
                throw new ArgumentOutOfRangeException("partIndexes");
            }

            string[] selectedParts = new string[partIndexes.Length];
            for (int i = 0; i < partIndexes.Length; i++)
            {
                selectedParts[i] = parts[partIndexes[i]];
            }

            return selectedParts;
        }

        /// <summary>
        /// Gets type by string
        /// </summary>
        /// <param name="value">Type name</param>
        /// <returns>Type if found otherwise null</returns>
        public static Type GetTypeByPartialName(this string value)
        {
            string[] valueParts = value.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string typeFullName = null;
            for (int i = 0; i < valueParts.Length; i++)
            {
                typeFullName = value + ", " + Concatenate(valueParts, '.', 0, i);
                Type type = Type.GetType(typeFullName);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether sring is Int32
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>True if string is numeric, otherwise False</returns>
        public static bool IsInteger(this string value)
        {
            int integerValue;
            return int.TryParse(value, out integerValue);
        }

        /// <summary>
        /// Determines whether sring is Int64
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>True if string is numeric, otherwise False</returns>
        public static bool IsLong(this string value)
        {
            long longValue;
            return long.TryParse(value, out longValue);
        }

        /// <summary>
        /// Determines whether sring is numeric
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>True if string is numeric, otherwise False</returns>
        public static bool IsNumeric(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsNumber(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether sring is Int16
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>True if string is numeric, otherwise False</returns>
        public static bool IsShort(this string value)
        {
            short shortValue;
            return short.TryParse(value, out shortValue);
        }

        /// <summary>
        /// Joing array of strings into string
        /// </summary>
        /// <param name="value">Array of strings for joining</param>
        /// <param name="separator">Separator for including between joined strings</param>
        /// <returns>Joined strings</returns>
        public static string Join(this string[] value, string separator = null)
        {
            if (value == null || value.Length == 0)
            {
                return null;
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                stringBuilder.Append(value[i]);
                if (i < value.Length - 1)
                {
                    stringBuilder.Append(separator);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Removes characters except alphanumeric
        /// </summary>
        /// <param name="value">String for conversion</param>
        /// <returns>Alphanumeric characters</returns>
        public static string RemoveCharactersExceptAlphaNumeric(this string value, bool capitalizeWords = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <value.Length; i++)
            {
                if (char.IsDigit(value[i]) || char.IsLetter(value[i]))
                {
                    stringBuilder.Append(value[i]);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Makes string compatible with c# variable naming convention
        /// </summary>
        /// <param name="value">String for conversion</param>
        /// <returns>C# variable</returns>
        public static string MakeCSharpCompatible(this string value, bool capitalizeWords = false)
        {
            // Do nothing if value wasn't provided
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            value = value.Replace(Environment.NewLine, string.Empty);

            char? previousChar = null;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    if (capitalizeWords && Char.IsLetter(value[i]))
                    {
                        value = StringExtensionMethods.ReplaceCharacter(value, i, Char.ToUpper(value[i]));
                    }
                }
                else
                {
                    switch (value[i])
                    {
                        case ' ':
                            if (capitalizeWords && previousChar != null && Char.IsLetter(previousChar.Value))
                            {
                                value = StringExtensionMethods.ReplaceCharacter(value, i + 1, Char.ToUpper(previousChar.Value));
                            }

                            value = StringExtensionMethods.ReplaceCharacter(value, i, string.Empty);
                            break;
                        case '-':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Dash");
                            previousChar = null;
                            break;
                        case '~':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Tilda");
                            previousChar = null;
                            break;
                        case '!':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "ExplanationMark");
                            previousChar = null;
                            break;
                        case '@':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "At");
                            previousChar = null;
                            break;
                        case '#':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Pound");
                            previousChar = null;
                            break;
                        case '$':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Dollar");
                            previousChar = null;
                            break;
                        case '%':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Percent");
                            previousChar = null;
                            break;
                        case '&':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Ampersand");
                            previousChar = null;
                            break;
                        case '*':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Multiply");
                            previousChar = null;
                            break;
                        case '(':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "LeftBracket");
                            previousChar = null;
                            break;
                        case ')':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "RightBracket");
                            previousChar = null;
                            break;
                        case '+':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Plus");
                            previousChar = null;
                            break;
                        case '=':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Equal");
                            previousChar = null;
                            break;
                        case '{':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "LeftCurlyBracket");
                            previousChar = null;
                            break;
                        case '}':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "RightCurlyBracket");
                            previousChar = null;
                            break;
                        case '|':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Pipeline");
                            previousChar = null;
                            break;
                        case '[':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "LeftSquareBracket");
                            previousChar = null;
                            break;
                        case ']':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "RightSquareBracket");
                            previousChar = null;
                            break;
                        case '\\':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "BackSlash");
                            previousChar = null;
                            break;
                        case ':':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Column");
                            previousChar = null;
                            break;
                        case '"':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "DoubleQuote");
                            previousChar = null;
                            break;
                        case ';':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "SemiColumn");
                            previousChar = null;
                            break;
                        case '\'':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "SingleQuote");
                            previousChar = null;
                            break;
                        case ',':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Comma");
                            previousChar = null;
                            break;
                        case '.':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Dot");
                            previousChar = null;
                            break;
                        case '/':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "Slash");
                            previousChar = null;
                            break;
                        case '?':
                            value = StringExtensionMethods.ReplaceCharacter(value, i, "QuestionMark");
                            previousChar = null;
                            break;
                        default:
                            previousChar = value[i];
                            break;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Removes HTML
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>String without HTML</returns>
        public static string RemoveHTML(this string value)
        {
            string pattern = @"<(.|\n)*?>";
            return Regex.Replace(value, pattern, string.Empty).Replace("&nbsp;", string.Empty);
        }

        /// <summary>
        /// Removes leading zeros, for example 0910110000
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>String with removed leading zeros, for example 910110000 from 0910110000</returns>
        public static string RemoveLeadingZeros(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '0')
                {
                    continue;
                }
                else
                {
                    return value.Substring(i);
                }
            }

            return value;
        }

        /// <summary>
        /// Removes trailing zeros, for example 123.456000
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="maxLength">Max length, for example 8</param>
        /// <returns>String with removed trailig zeros, for example 123.4560 if maxLength is 8 or 123.456 if max length is not specified</returns>
        public static string RemoveTrailingZeros(this string value, int? maxLength = null)
        {
            // Do nothing if value is not provided
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            // Do nothing if maxLength is more than value length
            if (maxLength.HasValue && maxLength > value.Length)
            {
                return value;
            }

            // Determining trailing zero position
            int trailingZeroPosition = 0;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (value[i] == '0')
                {
                    trailingZeroPosition = i;
                }
                else
                {
                    break;
                }
            }

            if (trailingZeroPosition > 0)
            {
                // Value contains trailing zero
                if (maxLength.HasValue && trailingZeroPosition < maxLength)
                {
                    // Removing trailing zeros which exceed max length
                    return value.Substring(0, maxLength.Value);
                }
                else
                {
                    // Removing all trailing zeros
                    return value.Substring(0, trailingZeroPosition);
                }
            }
            else
            {
                // Value doesn't contain trailing zero
                return value;
            }
        }

        /// <summary>
        /// Replaces characters by replacement
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="charactersForReplacement">Characters for replacement</param>
        /// <param name="replacement">Replacement string</param>
        /// <returns>String with replaced characters</returns>
        public static string Replace(this string value, char[] charactersForReplacement, string replacement)
        {
            string[] stringParts = value.Split(charactersForReplacement, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(replacement, stringParts);
        }

        /// <summary>
        /// Replaces character by provided string
        /// </summary>
        /// <param name="value">Value with character in question</param>
        /// <param name="position">Character position</param>
        /// <param name="replacement">Replacement string</param>
        /// <returns>String with replaced character</returns>
        private static string ReplaceCharacter(string value, int position, char replacement)
        {
            return value.Substring(0, position) + replacement + value.Substring(position + 1);
        }

        /// <summary>
        /// Replaces character by provided string
        /// </summary>
        /// <param name="value">Value with character in question</param>
        /// <param name="position">Character position</param>
        /// <param name="replacement">Replacement string</param>
        /// <returns>String with replaced character</returns>
        private static string ReplaceCharacter(string value, int position, string replacement)
        {
            return value.Substring(0, position) + replacement + value.Substring(position + 1);
        }

        /// <summary>
        /// Determines whether string starts from specified value
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="pattern">String for comparison (required)</param>
        /// <returns>True if string starts from specified value, otherwise False</returns>
        public static bool StartsWith(this string value, string pattern)
        {
            // Validate arguments
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException("pattern");
            }

            if (string.IsNullOrEmpty(value))
            {
                if (string.IsNullOrEmpty(pattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(pattern))
                {
                    return false;
                }
                else
                {
                    if (value.Length < pattern.Length)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            for (int j = 0; j < value.Length; j++)
                            {
                                if (pattern[i] != value[i])
                                {
                                    return false;
                                }
                            }
                        }

                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// Converts string to specified type
        /// </summary>
        /// <typeparam name="T">Output type</typeparam>
        /// <param name="value">String value for conversion</param>
        /// <returns>Strongly-typed value</returns>
        public static T To<T>(this string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Gets byte representation of string
        /// </summary>
        /// <param name="value">String value for conversion</param>
        /// <returns>Byte representation of string</returns>
        public static byte[] ToBytes(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Adjust value to camel notation
        /// </summary>
        /// <param name="value">Value for adjusting</param>
        /// <returns>Adjusted to camel notation value</returns>
        public static string ToCamelNotation(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
        }

        /// <summary>
        /// Converts value into list of distinct values
        /// </summary>
        /// <param name="value">Value for conversion</param>
        /// <param name="separator">Separator for value splitting</param>
        /// <returns>List of distinct values</returns>
        public static DistinctList<string> ToDistinctList(this string value, char separator)
        {
            DistinctList<string> result = new DistinctList<string>();
            if (string.IsNullOrWhiteSpace(value))
            {
                return result;
            }

            string[] valueParts = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < valueParts.Length; i++)
            {
                result.Add(valueParts[i].Trim());
            }

            return result;
        }

        /// <summary>
        /// Adjust value to English language
        /// </summary>
        /// <param name="value">Value for adjusting</param>
        /// <returns>Adjusted to file name value</returns>
        public static string ToEnglish(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            string charactersForReplacement = "àèìòùÀÈÌÒÙäëïöüÄËÏÖÜâêîôûÂÊÎÔÛáéíóúÁÉÍÓÚðÐýÝãñõÃÑÕšŠžŽçÇåÅøØ’";
            string replacingCharacters = "aeiouAEIOUaeiouAEIOUaeiouAEIOUaeiouAEIOUdDyYanoANOsSzZcCaAoO ";
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                char currentCharacter = value[i];
                int index = charactersForReplacement.IndexOf(currentCharacter);
                if (index >= 0)
                {
                    stringBuilder.Append(replacingCharacters[index]);
                }
                else
                {
                    stringBuilder.Append(value[i]);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Adjust value to file name
        /// </summary>
        /// <param name="value">Value for adjusting</param>
        /// <returns>Adjusted to file name value</returns>
        public static string ToFileName(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            string[] valueParts = value.Split(new char[] { ' ' });
            for (int i = 0; i < valueParts.Length; i++)
            {
                valueParts[i] = valueParts[i].ToPascalNotation();
            }

            return valueParts.Join().MakeCSharpCompatible();
        }

        /// <summary>
        /// Converts value into HTML
        /// </summary>
        /// <param name="value">Value for converting into HTML</param>
        /// <returns>Value for converted into HTML</returns>
        public static string ToHtml(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return value.Replace(Environment.NewLine, "</br>");
        }

        /// <summary>
        /// Converts value into list of distinct Order By instructions
        /// </summary>
        /// <param name="value">Value for conversion</param>
        /// <param name="separator">Separator for value splitting</param>
        /// <returns>List of distinct Order By instructions</returns>
        public static DistinctList<OrderByInstruction> ToOrderByDistinctListInstructions(this string value, char separator)
        {
            DistinctList<OrderByInstruction> result = new DistinctList<OrderByInstruction>();
            if (string.IsNullOrWhiteSpace(value))
            {
                return result;
            }

            string[] orderByParts = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < orderByParts.Length; i++)
            {
                OrderByInstruction orderByInstruction = new OrderByInstruction(orderByParts[i]);
                result.Add(orderByInstruction);
            }

            return result;
        }

        /// <summary>
        /// Converts value into list of distinct Order By names
        /// </summary>
        /// <param name="value">Value for conversion</param>
        /// <param name="separator">Separator for value splitting</param>
        /// <returns>List of distinct Order By names</returns>
        public static DistinctList<string> ToOrderByDistinctListOfNames(this string value, char separator)
        {
            DistinctList<string> result = new DistinctList<string>();
            if (string.IsNullOrWhiteSpace(value))
            {
                return result;
            }

            string[] orderByParts = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < orderByParts.Length; i++)
            {
                OrderByInstruction orderByInstruction = new OrderByInstruction(orderByParts[i]);
                result.Add(orderByInstruction.PropertyName);
            }

            return result;
        }

        /// <summary>
        /// Adjust value to Pascal notation
        /// </summary>
        /// <param name="value">Value for adjusting</param>
        /// <returns>Adjusted to pascal notation value</returns>
        public static string ToPascalNotation(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
        }

        /// <summary>
        /// Adjust value to sentence
        /// </summary>
        /// <param name="value">Value for adjusting</param>
        /// <returns>Adjusted to sentence value</returns>
        public static string ToSentence(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLetter(value[i]))
                {
                    if (i == 0)
                    {
                        // Handle the first character
                        if (Char.IsLower(value[i]))
                        {
                            // Convert the first lower case character to upper case
                            stringBuilder.Append(Char.ToUpperInvariant(value[i]));
                        }
                        else
                        {
                            // Use upper case character without conversion
                            stringBuilder.Append(value[i]);
                        }
                    }
                    else if (i == value.Length - 1)
                    {
                        // Handle the last character
                        if (Char.IsLower(value[i]))
                        {
                            // Use lower case character without conversion
                            stringBuilder.Append(value[i]);
                        }
                        else
                        {
                            // Look at the previous character
                            if (char.IsLetter(value[i - 1]))
                            {
                                if (Char.IsUpper(value[i - 1]))
                                {
                                    // Use upper case character without conversion because of acronim
                                    stringBuilder.Append(value[i]);
                                }
                                else
                                {
                                    // Convert upper case character to lower case
                                    stringBuilder.Append(Char.ToLowerInvariant(value[i]));
                                }
                            }
                            else
                            {
                                // Use upper case character without conversion because of acronim
                                stringBuilder.Append(value[i]);
                            }
                        }
                    }
                    else
                    {
                        // Handle the rest of characters
                        if (Char.IsLower(value[i]))
                        {
                            // Use lower case character without conversion
                            stringBuilder.Append(value[i]);
                        }
                        else
                        {
                            // Look at the next character
                            if (i < value.Length - 1)
                            {
                                if (char.IsLetter(value[i + 1]))
                                {
                                    if (Char.IsUpper(value[i + 1]))
                                    {
                                        // Use upper case character without conversion because of acronim
                                        stringBuilder.Append(value[i]);
                                    }
                                    else
                                    {
                                        // Convert upper case character to lower case
                                        stringBuilder.Append(Char.ToLowerInvariant(value[i]));
                                    }
                                }
                                else
                                {
                                    // Use upper case character without conversion because of acronim
                                    stringBuilder.Append(value[i]);
                                }
                            }
                            else
                            {
                                // Convert the last upper case character to lower case
                                stringBuilder.Append(Char.ToLowerInvariant(value[i]));
                            }
                        }
                    }
                }
                else
                {
                    // Append non-letter character
                    stringBuilder.Append(value[i]);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Adjust value to title
        /// </summary>
        /// <param name="value">Value for adjusting</param>
        /// <returns>Adjusted to title value</returns>
        public static string ToTitle(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            bool previousCharacterWasSpace = false;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLetter(value[i]))
                {
                    if (i == 0)
                    {
                        // Handle the first character
                        if (Char.IsLower(value[i]))
                        {
                            // Convert the first lower case character to upper case
                            stringBuilder.Append(Char.ToUpperInvariant(value[i]));
                        }
                        else
                        {
                            // Use upper case character without conversion
                            stringBuilder.Append(value[i]);
                        }
                    }
                    else
                    {
                        if (previousCharacterWasSpace)
                        {
                            previousCharacterWasSpace = false;
                            if (Char.IsLower(value[i]))
                            {
                                // Convert the first lower case character to upper case
                                stringBuilder.Append(Char.ToUpperInvariant(value[i]));
                            }
                            else
                            {
                                // Use upper case character without conversion
                                stringBuilder.Append(value[i]);
                            }
                        }
                        else
                        {
                            // Use upper case character without conversion
                            stringBuilder.Append(value[i]);
                        }
                    }
                }
                else if (char.IsWhiteSpace(value[i]))
                {
                    previousCharacterWasSpace = true;
                    stringBuilder.Append(value[i]);
                }
            }

            return stringBuilder.ToString();
        }
        #endregion
    }
}
