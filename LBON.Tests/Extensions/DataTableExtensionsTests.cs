using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using LBON.Extensions;
using Shouldly;
using Xunit;

namespace LBON.Tests.Extensions
{
    public class DataTableExtensionsTests
    {
        [Fact]
        public void ToXml_Test()
        {
            var dt = new DataTable();
            var xml = dt.ToXml("Test");
            xml.NodeType.ShouldBe(XmlNodeType.Document);
        }
    }
}
