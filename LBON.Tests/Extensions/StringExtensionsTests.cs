using System;
using System.Collections.Generic;
using System.Text;
using LBON.Extensions;
using Shouldly;
using Xunit;

namespace LBON.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null,"123")]
        public void IsNullOrWhiteSpace_Test(string valueOne, string valueTwo)
        {
            var result1 = valueOne.IsNullOrWhiteSpace();
            var result2 = valueTwo.IsNullOrWhiteSpace();
            result1.ShouldBeTrue();
            result2.ShouldBeFalse();
        }

        [Theory]
        [InlineData("123")]
        public void Copy_Test(string value)
        {
            var result = value.Copy();
            result.ShouldBe(value);
        }

        [Theory]
        [InlineData("测试测试158777788882218900001111测试测试")]
        public void GetPhoneNumber_Test(string value)
        {
            var result = value.GetPhoneNumber();
            var result1 = value.GetPhoneNumbers();
            result.ShouldBe("15877778888");
            result1.Count.ShouldBe(2);
        }

        [Fact]
        public void ToInt_Test()
        {
            const string value1 = "1";
            const string value2 = "0";
            const string value3 = "-1";
            var result1 = value1.ToInt();
            var result2 = value2.ToInt();
            var result3 = value3.ToInt();
            result1.ShouldBe(1);
            result2.ShouldBe(0);
            result3.ShouldBe(-1);
        }

        [Fact]
        public void ToDecimal_Test()
        {
            const string value1 = "1";
            const string value2 = "0.001";
            const string value3 = "-1.01";
            var result1 = value1.ToDecimal();
            var result2 = value2.ToDecimal();
            var result3 = value3.ToDecimal();
            result1.ShouldBe(1);
            result2.ShouldBe((decimal)0.001);
            result3.ShouldBe((decimal)-1.01);
        }

        [Theory]
        [InlineData("12.111")]
        [InlineData("-12.111")]
        [InlineData("0.01")]
        [InlineData("0")]
        public void IsNumeric_Test1(string value)
        {
            var result = value.IsNumeric();
            result.ShouldBeTrue();
        }

        [Theory]
        [InlineData("-12..111")]
        [InlineData("-12.11.1")]
        [InlineData("12大户")]
        public void IsNumeric_Test2(string value)
        {
            var result = value.IsNumeric();
            result.ShouldBeFalse();
        }
    }
}
