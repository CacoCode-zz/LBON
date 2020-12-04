using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;
using LBON.Extensions;

namespace LBON.Tests.Extensions
{
    public class DecimalExtensionsTests
    {
        [Theory]
        [InlineData(123.123, 123)]
        public void ToChineseAmount_Test(decimal value,int intValue)
        {
            var result1 = value.ToChineseAmount();
            var result2 = intValue.ToChineseAmount();
            result1.ShouldBe("壹佰贰拾叁元壹角贰分");
            result2.ShouldBe("壹佰贰拾叁元");
        }
    }
}
