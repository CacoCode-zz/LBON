# Lu Ban Of .Net - .NET 鲁班工匠辅助库
[![Github Build Status](https://github.com/CacoCode/LBON/workflows/LBON_MASTER/badge.svg?branch=master)](https://github.com/CacoCode/LBON/actions?query=workflow%3ALBON_MASTER+branch%3Amaster)

# 简介
.Net 高可用、高效率的扩展组件，希望对 .NET 开发者及爱好者带来便捷，告别996，远离ICU！！！

# Nuget

| **名称** |      **Nuget**      |
|----------|:-------------:|
| **LBON.Consts** | **[![NuGet](https://buildstats.info/nuget/LBON.Consts)](https://www.nuget.org/packages/LBON.Consts)** |
| **LBON.Extensions** | **[![NuGet](https://buildstats.info/nuget/LBON.Extensions)](https://www.nuget.org/packages/LBON.Extensions)** |

# 功能模块(1.0.2)
## Extensions
- ### String Extensions
    - <code>IsNullOrWhiteSpace</code>
    - <code>IsNullOrEmpty</code>
    - <code>Copy</code>
    - <code>GetPhoneNumber</code>
    - <code>RegexMatch</code>
    - <code>ToInt</code>
    - <code>ToEnum</code>
    - <code>ToSecureString</code>
    - <code>ToDecimal</code>
    - <code>ToCamelCase</code>
    - <code>ToBytes</code> &nbsp;&nbsp;&nbsp; 通过文件路径把文件转byte[]
    - <code>ToColor</code>
    - <code>IsNumeric</code>
    - <code>IsContains</code>
    - <code>IsDate</code>
    - <code>IsEmailAddress</code>
    - <code>IsGuid</code>
    - <code>IsUrl</code>
    - <code>IsLengthAtLeast</code>
    - <code>IsStrongPassword</code>
    - <code>IsMatchRegex</code>
    - <code>In</code>
    - <code>Parse</code>
    - <code>Format</code>
    - <code>Encrypt</code>
    - <code>Decrypt</code>
    - <code>Mask</code> &nbsp;&nbsp;&nbsp; "123456" to "12**56"
    - <code>MaskMobile</code>
    - <code>MaskIdCard</code>
    - <code>MaskBankCard</code>
    - <code>FirstToUpper</code>
    - <code>PopL</code>
    - <code>PopR</code>
- ### DataTable Extensions
    - <code>ToList</code>
    - <code>ToDataTable\<T>(this ICollection\<T> source)</code>
    - <code>ToXml</code>
    - <code>SelectDistinct</code>
    - <code>SelectRows</code>
    - <code>Duplicate</code> &nbsp;&nbsp;&nbsp; 删除基于任何列的重复行
    - <code>EqualsByContent</code> &nbsp;&nbsp;&nbsp; 检查两个DataTable对象是否具有相同的内
    - <code>RenameColumn</code>
    - <code>RemoveColumn</code>
- ### Image Extensions
    - <code>ToBase64</code>
    - <code>ToBytes</code>
    - <code>ToImage</code>
    - <code>ToIcoStream</code>
    - <code>ScaleImage</code>
- ### DecimalOrInt Extensions 
    - <code>ToChineseAmount</code>
    - <code>IsBetween</code>
- ### Object Extensions 
    - <code>GetDisplayName</code>
    - <code>GetDescription</code>
    - <code>GetTypeDisplayOrDescription</code>
    - <code>IsRequired</code>
    - <code>GetAttribute</code>
    - <code>EnumToList</code>
    - <code>EnumToDictionary</code>
- ### Xml Extensions 
    - <code>FromXml</code>
    - <code>Deserialize</code>
- ### Enumerable Extensions 
    - <code>Join</code>
- ### DateTime Extensions 
    - <code>FirstOfMonth</code>
    - <code>LastOfMonth</code>
## Consts
- ### [MediaType](LBON.Consts/MediaTypeConst.cs)
- ### [Regex](LBON.Consts/RegexConst.cs)

    
