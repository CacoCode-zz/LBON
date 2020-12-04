using System;

namespace LBON.Consts
{
    public class RegexConst
    {
        public const string PhoneNumber = "1[3|4|5|7|8|9][0-9]{9}";

        public const string IsNumeric = @"^[+-]?\d+[.]?\d*$";

        public const string Email = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        public const string Url = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?";

            
    }
}
