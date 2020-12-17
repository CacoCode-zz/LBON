using System;

namespace LBON.Consts
{
    public class RegexConst
    {
        public const string PhoneNumber = "1[3|4|5|7|8|9][0-9]{9}";

        public const string IsNumeric = @"^[+-]?\d+[.]?\d*$";

        public const string Ip = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";

        public const string Email = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        public const string Url = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$";

        public const string Date = @"(\d{4})-(\d{1,2})-(\d{1,2})";

        public const string ZipCode = @"^\d{6}$";
    }
}
