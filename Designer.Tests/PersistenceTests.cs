using System.IO;
using System.Threading.Tasks;
using Designer.Core.Persistence;
using Xunit;
using Zafiro.Core.Mixins;

namespace Designer.Tests
{
    public class PersistenceTests
    {
        [Fact]
        public async Task SaveTest()
        {
            var sut = new ProjectStore();
            using (var stream = new MemoryStream())
            {
                await sut.Save(ProjectBuilder.Build(), stream);
                stream.Seek(0, SeekOrigin.Begin);
                var str = await stream.ReadToEnd();
            }
        }

        [Fact]
        public async Task ReadTest()
        {
            var source =
                @"<?xml version=""1.0"" encoding=""utf-8""?><Project xmlns:sys=""https://extendedxmlserializer.github.io/system"" xmlns=""clr-namespace:Designer.Domain.Models;assembly=Designer.Domain.Models""><Documents Capacity=""4""><Document><Items Capacity=""4""><EllipseShape xmlns:exs=""https://extendedxmlserializer.github.io/v2"" exs:identity=""1"" Id=""1"" HorizontalRadius=""0"" VerticalRadius=""0"" Left=""0"" Top=""0"" /><EllipseShape xmlns:exs=""https://extendedxmlserializer.github.io/v2"" exs:identity=""2"" Id=""2"" HorizontalRadius=""0"" VerticalRadius=""0"" Left=""0"" Top=""0"" /><WheelJoint Left=""0"" Top=""0""><FirstBody xmlns:exs=""https://extendedxmlserializer.github.io/v2"" exs:type=""EllipseShape"" exs:reference=""1"" /><SecondBody xmlns:exs=""https://extendedxmlserializer.github.io/v2"" exs:type=""EllipseShape"" exs:reference=""2"" /></WheelJoint></Items></Document></Documents></Project>";

            using (var stream = source.ToStream())
            {
                var sut = new ProjectStore();
                var project = await sut.Load(stream);
            }
        }
    }
}