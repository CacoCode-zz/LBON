using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using LBON.Consts;
using LBON.Extensions.Enums;

namespace LBON.Extensions
{
    public static class StringExtensions
    {
        /// <summary>Determines whether [is null or white space].</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is null or white space] [the specified string]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>Determines whether [is null or empty].</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified string]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>Copies the specified string.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string Copy(this string str)
        {
            return string.Copy(str);
        }

        /// <summary>Gets the phone number.</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string GetPhoneNumber(this string str, string pattern = RegexConst.PhoneNumber)
        {
            var reg = new Regex(pattern);
            var match = reg.Match(str);
            if (match.Success)
            {
                return match.Value;
            }
            return null;
        }

        /// <summary>Gets the phone numbers.</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static List<string> GetPhoneNumbers(this string str, string pattern = RegexConst.PhoneNumber)
        {
            var reg = new Regex(pattern);
            var matches = reg.Matches(str);
            return (from Match item in matches select item.Value).ToList();
        }

        /// <summary>Regex Match the specified pattern.</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string RegexMatch(this string str, string pattern)
        {
            var reg = new Regex(pattern);
            var match = reg.Match(str);
            if (match.Success)
            {
                return match.Value;
            }
            return null;
        }

        /// <summary>Regex Matches the specified pattern.</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static List<string> RegexMatches(this string str, string pattern)
        {
            var reg = new Regex(pattern);
            var matches = reg.Matches(str);
            return (from Match item in matches select item.Value).ToList();
        }

        /// <summary>Converts the string representation of a number to an integer.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        /// <summary>Converts to decimal.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static decimal ToDecimal(this string str)
        {
            return decimal.Parse(str);
        }

        /// <summary>Determines whether this instance is numeric.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(this string str)
        {
            var regex = new Regex(RegexConst.IsNumeric);
            return regex.IsMatch(str);
        }

        /// <summary>Pops the l.</summary>
        /// <param name="str">The string.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static string PopL(this string str, int count = 1)
        {
            if (count > str.Length || count < 0)
                throw new IndexOutOfRangeException();
            return str.Substring(count);
        }

        /// <summary>Pops the r.</summary>
        /// <param name="str">The string.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static string PopR(this string str, int count = 1)
        {
            if (count > str.Length || count < 0)
                throw new IndexOutOfRangeException();
            return str.Substring(0, str.Length - count);
        }

        /// <summary>Determines whether the specified value is contains.</summary>
        /// <param name="str">The string.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is contains; otherwise, <c>false</c>.</returns>
        public static bool IsContains(this string str, string value)
        {
            return str.IndexOf(value, StringComparison.Ordinal) >= 0;
        }

        /// <summary>Ins the specified string values.</summary>
        /// <param name="str">The string.</param>
        /// <param name="stringValues">The string values.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static bool In(this string str, params string[] stringValues)
        {
            foreach (string otherValue in stringValues)
                if (string.CompareOrdinal(str, otherValue) == 0)
                    return true;

            return false;
        }

        /// <summary>Converts to enum.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static T ToEnum<T>(this string str)
            where T : struct
        {
            return (T)Enum.Parse(typeof(T), str, true);
        }

        /// <summary>Formats the specified arg0.</summary>
        /// <param name="str">The string.</param>
        /// <param name="arg0">The arg0.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string Format(this string str, object arg0)
        {
            return string.Format(str, arg0);
        }

        /// <summary>Formats the specified arguments.</summary>
        /// <param name="str">The string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>Encrypts the specified key.</summary>
        /// <param name="str">The string.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="ArgumentException">An empty string value cannot be encrypted.
        /// or
        /// Cannot encrypt using an empty key. Please supply an encryption key.</exception>
        public static string Encrypt(this string str, string key)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
            }
            var cspParameters = new CspParameters {KeyContainerName = key};
            var rsa = new RSACryptoServiceProvider(cspParameters) {PersistKeyInCsp = true};
            var bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(str), true);
            return BitConverter.ToString(bytes);
        }

        /// <summary>Decrypts the specified key.</summary>
        /// <param name="str">The string.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="ArgumentException">An empty string value cannot be encrypted.
        /// or
        /// Cannot decrypt using an empty key. Please supply a decryption key.</exception>
        public static string Decrypt(this string str, string key)
        {
            string result = null;
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
            }
            var cspParameters = new CspParameters {KeyContainerName = key};
            var rsa = new RSACryptoServiceProvider(cspParameters) {PersistKeyInCsp = true};
            var decryptArray = str.Split(new string[] { "-" }, StringSplitOptions.None);
            var decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (s => Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));
            var bytes = rsa.Decrypt(decryptByteArray, true);
            result = Encoding.UTF8.GetString(bytes);
            return result;
        }

        /// <summary>Firsts to upper.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string FirstToUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            var theChars = str.ToCharArray();
            theChars[0] = char.ToUpper(theChars[0]);
            return new string(theChars);
        }

        /// <summary>Converts to securestring.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static SecureString ToSecureString(this string str)
        {
            var secureString = new SecureString();
            foreach (var c in str)
                secureString.AppendChar(c);

            return secureString;
        }

        /// <summary>Determines whether this instance is date.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is date; otherwise, <c>false</c>.</returns>
        public static bool IsDate(this string str)
        {
            return !string.IsNullOrEmpty(str) && DateTime.TryParse(str, out _);
        }

        /// <summary>Converts to camelcase.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string ToCamelCase(this string str)
        {
            if (str == null || str.Length < 2)
                return str;

            var words = str.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            var result = words[0].ToLower();
            for (var i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }

        /// <summary>Determines whether [is email address].</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is email address] [the specified string]; otherwise, <c>false</c>.</returns>
        public static bool IsEmailAddress(this string str)
        {
            var regex = new Regex(RegexConst.Email);
            return regex.IsMatch(str);
        }

        /// <summary>Parses the specified string.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static T Parse<T>(this string str)
        {
            var result = default(T);
            if (!string.IsNullOrEmpty(str))
            {
                var tc = TypeDescriptor.GetConverter(typeof(T));
                result = (T)tc.ConvertFrom(str);
            }
            return result;
        }

        /// <summary>Determines whether this instance is unique identifier.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is unique identifier; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsGuid(this string str)
        {
            if (str == null)
                throw new ArgumentNullException();

            var format = new Regex(
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            var match = format.Match(str);
            return match.Success;
        }

        /// <summary>Determines whether this instance is URL.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is URL; otherwise, <c>false</c>.</returns>
        public static bool IsUrl(this string str)
        {
            var regex = new Regex(RegexConst.Url);
            return regex.IsMatch(str);
        }

        /// <summary>Masks the specified number exposed.</summary>
        /// <param name="str">The string.</param>
        /// <param name="numExposed">The number exposed.</param>
        /// <param name="maskChar">The mask character.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string Mask(this string str, int numExposed, char maskChar = '*', MaskTypeEnum type = MaskTypeEnum.All)
        {
            var maskedString = str;

            if (str.IsLengthAtLeast(numExposed))
            {
                var builder = new StringBuilder(str.Length);
                int index = maskedString.Length - numExposed;

                if (type == MaskTypeEnum.AlphaNumericOnly)
                {
                    CreateAlphaNumMask(builder, str, maskChar, index);
                }
                else
                {
                    builder.Append(maskChar, index);
                }

                builder.Append(str.Substring(index));
                maskedString = builder.ToString();
            }

            return maskedString;
        }

        /// <summary>
        /// Masks the mobile.
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        /// <returns></returns>
        public static string MaskMobile(this string mobile)
        {
            if (!mobile.IsNullOrEmpty() && mobile.Length > 7)
            {
                var regex = new Regex(@"(?<=\d{3}).+(?=\d{4})", RegexOptions.IgnoreCase);
                mobile = regex.Replace(mobile, "****");
            }

            return mobile;
        }

        /// <summary>
        /// Masks the identifier card.
        /// </summary>
        /// <param name="idCard">The identifier card.</param>
        /// <returns></returns>
        public static string MaskIdCard(this string idCard)
        {
            if (!idCard.IsNullOrEmpty() && idCard.Length > 10)
            {
                var regex = new Regex(@"(?<=\w{6}).+(?=\w{4})", RegexOptions.IgnoreCase);
                idCard = regex.Replace(idCard, "********");
            }

            return idCard;
        }

        /// <summary>
        /// Masks the bank card.
        /// </summary>
        /// <param name="bankCard">The bank card.</param>
        /// <returns></returns>
        public static string MaskBankCard(this string bankCard)
        {
            if (!bankCard.IsNullOrEmpty() && bankCard.Length > 4)
            {
                var regex = new Regex(@"(?<=\d{4})\d+(?=\d{4})", RegexOptions.IgnoreCase);
                bankCard = regex.Replace(bankCard, " **** **** ");
            }

            return bankCard;
        }

        /// <summary>Determines whether [is length at least] [the specified length].</summary>
        /// <param name="str">The string.</param>
        /// <param name="length">The length.</param>
        /// <returns>
        ///   <c>true</c> if [is length at least] [the specified length]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">length - The length must be a non-negative number.</exception>
        public static bool IsLengthAtLeast(this string str, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length,
                    "The length must be a non-negative number.");
            }

            return str != null && str.Length >= length;
        }

        /// <summary>Determines whether [is strong password].</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is strong password] [the specified string]; otherwise, <c>false</c>.</returns>
        public static bool IsStrongPassword(this string str)
        {
            var isStrong = Regex.IsMatch(str, @"[\d]");
            if (isStrong) isStrong = Regex.IsMatch(str, @"[a-z]");
            if (isStrong) isStrong = Regex.IsMatch(str, @"[A-Z]");
            if (isStrong) isStrong = Regex.IsMatch(str, @"[\s~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]");
            if (isStrong) isStrong = str.Length > 7;
            return isStrong;
        }

        /// <summary>Determines whether [is match regex] [the specified pattern].</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <c>true</c> if [is match regex] [the specified pattern]; otherwise, <c>false</c>.</returns>
        public static bool IsMatchRegex(this string str, string pattern)
        {
            var regex = new Regex(pattern);
            return regex.IsMatch(str);
        }

        /// <summary>Converts to bytes.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static byte[] ToBytes(this string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            return File.ReadAllBytes(fileName);
        }

        /// <summary>Converts to color.</summary>
        /// <param name="rgb">The RGB.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static Color ToColor(this string rgb)
        {
            rgb = rgb.Replace("#", "");
            var a = Convert.ToByte("ff", 16);
            byte pos = 0;
            if (rgb.Length == 8)
            {
                a = Convert.ToByte(rgb.Substring(pos, 2), 16);
                pos = 2;
            }
            var r = Convert.ToByte(rgb.Substring(pos, 2), 16);
            pos += 2;
            var g = Convert.ToByte(rgb.Substring(pos, 2), 16);
            pos += 2;
            var b = Convert.ToByte(rgb.Substring(pos, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }

        private static void CreateAlphaNumMask(StringBuilder buffer, string source, char mask, int length)
        {
            for (int i = 0; i < length; i++)
            {
                buffer.Append(char.IsLetterOrDigit(source[i])
                                ? mask
                                : source[i]);
            }
        }

    }
}
