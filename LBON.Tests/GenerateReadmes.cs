using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace LBON.Tests
{
    public class GenerateReadmes
    {
        [Fact]
        public void Run_Test()
        {
            GenerateExtensionsReadmes();
            GenerateHeleperReadmes();
        }

        private void GenerateExtensionsReadmes()
        {
            var classes = Assembly.Load("LBON.Extensions").GetTypes().Where(a => a.Name.EndsWith("Extensions")).ToList();
            foreach (var item in classes)
            {
                var dirPath = "E:\\Codes\\LBON\\Readmes\\Extensions";
                var filePath = Path.Combine(dirPath, $"{item.Name.ToUpper()}_README.md");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    string content = $"# {item.Name}";
                    var methods = item.GetMethods().Where(a => a.IsPublic && a.IsStatic).ToList();
                    foreach (var method in methods)
                    {
                        content += @$"
- <code>{method.Name}</code> {method.GetCustomAttribute<DescriptionAttribute>()?.Description}";
                    }
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    fileStream.Write(data, 0, data.Length);
                }
            }
        }

        private void GenerateHeleperReadmes()
        {
            var classes = Assembly.Load("LBON.Helper").GetTypes().Where(a => a.Name.EndsWith("Helper")).ToList();
            foreach (var item in classes)
            {
                var dirPath = "E:\\Codes\\LBON\\Readmes\\Helper";
                var filePath = Path.Combine(dirPath, $"{item.Name.ToUpper()}_README.md");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    string content = $"# {item.Name}";
                    var methods = item.GetMethods().Where(a => a.IsPublic && a.IsStatic).ToList();
                    foreach (var method in methods)
                    {
                        content += @$"
- <code>{method.Name}</code>";
                    }
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    fileStream.Write(data, 0, data.Length);
                }
            }
        }
    }
}
