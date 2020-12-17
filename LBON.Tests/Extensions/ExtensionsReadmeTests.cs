using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace LBON.Tests.Extensions
{
    public class ExtensionsReadmeTests
    {
        [Fact]
        public void Run_Test()
        {
            var classes = Assembly.Load("LBON.Extensions").GetTypes().Where(a => a.Name.EndsWith("Extensions")).ToList();
            foreach (var item in classes)
            {
                var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "readmes");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var filePath = Path.Combine(dirPath, $"{item.Name.ToUpper()}_README.md");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    string content = $"# {item.Name}";
                    var methods = item.GetMethods().Where(a => a.IsPublic).ToList();
                    foreach (var method in methods)
                    {
                        content += @$"
- <code>{method.Name}</code>";
                    }
                    byte[] data = Encoding.ASCII.GetBytes(content);
                    fileStream.Write(data, 0, data.Length);
                }
            }
        }
    }
}
