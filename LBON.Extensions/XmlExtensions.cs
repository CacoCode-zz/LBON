using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LBON.Extensions
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        [Description("将XML转换为Object")]
        public static object FromXml<T>(this string xml) where T : new()
        {
            using (var sr = new StringReader(xml))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(sr);
            }
        }

        /// <summary>
        /// Deserializes the specified XML document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlDocument">The XML document.</param>
        /// <returns></returns>
        [Description("反序列化指定的XML文档")]
        public static T Deserialize<T>(this XDocument xmlDocument)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var reader = xmlDocument.CreateReader())
                return (T)xmlSerializer.Deserialize(reader);
        }

        /// <summary>
        /// Gets an attribute's value from an Xml node.
        /// </summary>
        /// <param name="node">The Xml node</param>
        /// <param name="attributeName">Attribute name</param>
        /// <returns>Value of the attribute</returns>
        [Description("从Xml节点获取属性的值")]
        public static string GetAttributeValueOrNull(this XmlNode node, string attributeName)
        {
            if (node.Attributes == null || node.Attributes.Count <= 0)
            {
                throw new Exception(node.Name + " node has not " + attributeName + " attribute");
            }

            return node.Attributes
                .Cast<XmlAttribute>()
                .Where(attr => attr.Name == attributeName)
                .Select(attr => attr.Value)
                .FirstOrDefault();
        }
    }
}
