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
        public async Task LoadTest()
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
                @"<?xml version=""1.0"" encoding=""utf-8""?><Project xmlns:exs=""https://extendedxmlserializer.github.io/v2"" xmlns:sys=""https://extendedxmlserializer.github.io/system"" xmlns=""clr-namespace:Designer.Domain.Models;assembly=Designer.Domain.Models""><Documents exs:type=""sys:List[Document]"" Capacity=""4""><Document><Graphics exs:type=""sys:List[Item]"" Capacity=""4""><EllipseShape exs:identity=""1"" Id=""1"" HorizontalRadius=""0"" VerticalRadius=""0"" Left=""0"" Top=""0"" /><EllipseShape exs:identity=""2"" Id=""2"" HorizontalRadius=""0"" VerticalRadius=""0"" Left=""0"" Top=""0"" /><WheelJoint Left=""0"" Top=""0""><FirstBody exs:type=""EllipseShape"" exs:reference=""1"" /><SecondBody exs:type=""EllipseShape"" exs:reference=""2"" /></WheelJoint></Graphics></Document></Documents></Project>";

            using (var stream = source.ToStream())
            {
                var sut = new ProjectStore();
                var project = await sut.Load(stream);
            }
        }
    }
}