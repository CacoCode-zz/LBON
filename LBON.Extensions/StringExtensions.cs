using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
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
        [Description("检查是否为NULL或空格")]
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>Determines whether [is null or empty].</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified string]; otherwise, <c>false</c>.</returns>
        [Description("检查是否为NULL或空")]
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>Copies the specified string.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("复制")]
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
        [Description("获取单个手机号")]
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
        [Description("获取全部手机号")]
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
        [Description("正则匹配单个")]
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
        [Description("正则匹配多个")]
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
        [Description("String转换Int")]
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        /// <summary>Converts to decimal.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("String转换Decimal")]
        public static decimal ToDecimal(this string str)
        {
            return decimal.Parse(str);
        }

        /// <summary>Determines whether this instance is numeric.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is numeric; otherwise, <c>false</c>.</returns>
        [Description("是否为数字")]
        public static bool IsNumeric(this string str)
        {
            var regex = new Regex(RegexConst.IsNumeric);
            return regex.IsMatch(str);
        }

        /// <summary>Determines whether the specified value is contains.</summary>
        /// <param name="str">The string.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is contains; otherwise, <c>false</c>.</returns>
        [Description("是否包含")]
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
        [Description("是否存在数组中")]
        public static bool In(this string str, params string[] stringValues)
        {
            foreach (string otherValue in stringValues)
                if (string.CompareOrdinal(str, otherValue) == 0)
                    return true;

            return false;
        }

        /// <summary>Formats the specified arg0.</summary>
        /// <param name="str">The string.</param>
        /// <param name="arg0">The arg0.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("格式化")]
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
        [Description("RSA加密")]
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
            var cspParameters = new CspParameters { KeyContainerName = key };
            var rsa = new RSACryptoServiceProvider(cspParameters) { PersistKeyInCsp = true };
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
        [Description("RSA解密")]
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
            var cspParameters = new CspParameters { KeyContainerName = key };
            var rsa = new RSACryptoServiceProvider(cspParameters) { PersistKeyInCsp = true };
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
        [Description("首字母大写")]
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
        [Description("转换为安全字符串")]
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
        [Description("转换为日期")]
        public static bool IsDate(this string str)
        {
            return !string.IsNullOrEmpty(str) && DateTime.TryParse(str, out _);
        }

        /// <summary>Determines whether [is email address].</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is email address] [the specified string]; otherwise, <c>false</c>.</returns>
        [Description("是否为邮箱地址")]
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
        [Description("转换为任何格式")]
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
        [Description("是否为Guid")]
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
        [Description("是否为地址")]
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
        [Description("屏蔽字符，如：123***789")]
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
        [Description("屏蔽手机号")]
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
        [Description("屏蔽身份证")]
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
        [Description("屏蔽银行卡")]
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
        [Description("判断是否为最后一位字符")]
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
        [Description("判断是否为强壮密码")]
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
        [Description("是否正则匹配通过")]
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
        [Description("文件物理地址转换为字节数组")]
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
        [Description("转换为Color")]
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

        /// <summary>
        /// Adds a char to end of given string if it does not ends with the char.
        /// </summary>
        [Description("如果给定字符串不以[char]结尾，则在其结尾添加[char]")]
        public static string EnsureEndsWith(this string str, char c)
        {
            return EnsureEndsWith(str, c, StringComparison.Ordinal);
        }

        /// <summary>
        /// Adds a char to end of given string if it does not ends with the char.
        /// </summary>
        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.EndsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// Adds a char to end of given string if it does not ends with the char.
        /// </summary>
        public static string EnsureEndsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.EndsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// Adds a char to beginning of given string if it does not starts with the char.
        /// </summary>
        [Description("如果给定字符串不以[char]开头，则在其开头添加[char]")]
        public static string EnsureStartsWith(this string str, char c)
        {
            return EnsureStartsWith(str, c, StringComparison.Ordinal);
        }

        /// <summary>
        /// Adds a char to beginning of given string if it does not starts with the char.
        /// </summary>
        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.StartsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// Adds a char to beginning of given string if it does not starts with the char.
        /// </summary>
        public static string EnsureStartsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.StartsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
        [Description("从字符串的开头获取指定长度字符串")]
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

        /// <summary>
        /// Converts line endings in the string to <see cref="Environment.NewLine"/>.
        /// </summary>
        [Description("将字符串中的行尾转换为Environment.NewLine")]
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// Gets index of nth occurence of a char in a string.
        /// </summary>
        /// <param name="str">source string to be searched</param>
        /// <param name="c">Char to search in <paramref name="str"/></param>
        /// <param name="n">Count of the occurence</param>
        [Description("获取字符串中第n个字符的索引")]
        public static int NthIndexOf(this string str, char c, int n)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            var count = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                if ((++count) == n)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Removes first occurrence of the given postfixes from end of the given string.
        /// Ordering is important. If one of the postFixes is matched, others will not be tested.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="postFixes">one or more postfix.</param>
        /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
        [Description("从给定字符串的末尾删除第一个出现的给定后缀")]
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            if (postFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        /// Removes first occurrence of the given prefixes from beginning of the given string.
        /// Ordering is important. If one of the preFixes is matched, others will not be tested.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="preFixes">one or more prefix.</param>
        /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
        [Description("从给定字符串的开头移除第一个出现的给定前缀")]
        public static string RemovePreFix(this string str, params string[] preFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            if (preFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var preFix in preFixes)
            {
                if (str.StartsWith(preFix))
                {
                    return str.Right(str.Length - preFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        /// Gets a substring of a string from end of the string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
        [Description("从字符串的结尾获取指定长度字符串")]
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        [Description("字符串拆分")]
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        [Description("字符串换行拆分")]
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// Uses string.Split method to split given string by separator.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string[] SplitToLines(this string str, string separator)
        {
            return str.Split(separator);
        }

        /// <summary>
        /// Converts PascalCase string to camelCase string.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="invariantCulture">Invariant culture</param>
        /// <returns>camelCase of the string</returns>
        [Description("将PascalCase字符串转换为camelCase字符串")]
        public static string ToCamelCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToLowerInvariant() : str.ToLower();
            }

            return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0])) + str.Substring(1);
        }

        /// <summary>
        /// Converts PascalCase string to camelCase string in specified culture.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="culture">An object that supplies culture-specific casing rules</param>
        /// <returns>camelCase of the string</returns>
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToLower();
            }

            return char.ToLower(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Converts given PascalCase/camelCase string to sentence (by splitting words by space).
        /// Example: "ThisIsSampleSentence" is converted to "This is a sample sentence".
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <param name="invariantCulture">Invariant culture</param>
        [Description("将给定的PascalCase/camelCase字符串转换为句子（通过按空格拆分单词）")]
        public static string ToSentenceCase(this string str, bool invariantCulture = false)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(
                str,
                "[a-z][A-Z]",
                m => m.Value[0] + " " + (invariantCulture ? char.ToLowerInvariant(m.Value[1]) : char.ToLower(m.Value[1]))
            );
        }

        /// <summary>
        /// Converts given PascalCase/camelCase string to sentence (by splitting words by space).
        /// Example: "ThisIsSampleSentence" is converted to "This is a sample sentence".
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules.</param>
        public static string ToSentenceCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        /// <summary>
        /// Converts string to enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <returns>Returns enum object</returns>
        [Description("将字符串转换为枚举值")]
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Converts string to enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <param name="ignoreCase">Ignore case</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// Converts to md5.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        [Description("将字符串转换为MD5")]
        public static string ToMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Converts camelCase string to PascalCase string.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="invariantCulture">Invariant culture</param>
        /// <returns>PascalCase of the string</returns>
        [Description("将camelCase字符串转换为pascalase字符串")]
        public static string ToPascalCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToUpperInvariant() : str.ToUpper();
            }

            return (invariantCulture ? char.ToUpperInvariant(str[0]) : char.ToUpper(str[0])) + str.Substring(1);
        }

        /// <summary>
        /// Converts camelCase string to PascalCase string in specified culture.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="culture">An object that supplies culture-specific casing rules</param>
        /// <returns>PascalCase of the string</returns>
        public static string ToPascalCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToUpper();
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        [Description("如果指定长度超过最大长度，则从该字符串的开头获取指定长度的字符")]
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Left(maxLength);
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// It adds a "..." postfix to end of the string if it's truncated.
        /// Returning string can not be longer than maxLength.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// It adds given <paramref name="postfix"/> to end of the string if it's truncated.
        /// Returning string can not be longer than maxLength.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(str) || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }

        /// <summary>
        /// Read file context.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [Description("通过文件物理路径获取文件文本")]
        public static string ReadFile(this string filePath)
        {
            string context = string.Empty;
            if (!File.Exists(filePath))
            {
                throw new IOException($"'{filePath}'file not exist");
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    context = sr.ReadToEnd().ToString();
                }
            }
            return context;
        }

        /// <summary>
        /// Verify number sort
        /// </summary>
        /// <param name="intStr"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        [Description("验证文本数字，如'1,2,3'；是否按指定排序")]
        public static bool VerifySort(this string intStr, SortEnum sort = SortEnum.Asc)
        {
            var list = intStr.Split(",")
                .Select(int.Parse)
                .ToList();
            switch (sort)
            {
                case SortEnum.Asc:
                    list = list.OrderBy(a => a)
                    .ToList();
                    break;
                case SortEnum.Desc:
                    list = list.OrderByDescending(a => a)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
            }
                
            return string.Join(",", list) == intStr;
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
