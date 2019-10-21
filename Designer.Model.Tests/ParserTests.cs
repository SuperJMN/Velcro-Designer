using FluentAssertions;
using Superpower;
using Xunit;

namespace Designer.Model.Tests
{
    public class ParserTests
    {
        [InlineData(@"#----------------------------- Global Properties ----------------------------#
/outputDir:bin/$(Platform)
/intermediateDir:obj/$(Platform)
/platform:Windows
/config:
/profile:Reach
/compress:False
#---------------------------------- Content ---------------------------------#
#begin aerial_explosion.png
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:aerial_explosion.png
#begin Arial.spritefont
/importer:FontDescriptionImporter
/processor:FontDescriptionProcessor
/processorParam:PremultiplyAlpha=True
/processorParam:TextureFormat=Compressed
/build:Arial.spritefont",
            "")]
        [Theory]
        public void Test(string input, string expected)
        {
            var parser = new MonoGameProjectFileParser();
            var project = parser.Parse(input);
            project.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData("/outputDir:bin/$(Platform)", "/outputDir: bin/$(Platform)")]
        [InlineData("/processorParam:TextureFormat=Color", "/processorParam: TextureFormat = Color")]
        public void Statement(string input, string expected)
        {
            var st = Parsers.Statement.Parse(ProjectTokenizer.Create().Tokenize(input));
            st.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData(@"#begin Common/Buttons.png
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:Common/Buttons.png",
            @"{Common/Buttons.png}
/importer: TextureImporter
/processor: TextureProcessor
/processorParam: ColorKeyColor = 255,0,255,255
/processorParam: ColorKeyEnabled = True
/processorParam: GenerateMipmaps = False
/processorParam: PremultiplyAlpha = True
/processorParam: ResizeToPowerOfTwo = False
/processorParam: MakeSquare = False
/processorParam: TextureFormat = Color
/build: Common/Buttons.png")]
        public void ResourceEntry(string input, string expected)
        {
            var st = Parsers.ResourceEntry.Parse(ProjectTokenizer.Create().Tokenize(input));
            st.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData(@"#---------------------------------- Content ---------------------------------#
#begin aerial_explosion.png
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:aerial_explosion.png",

            @"{Content}
{aerial_explosion.png}
/importer: TextureImporter
/processor: TextureProcessor
/processorParam: ColorKeyColor = 255,0,255,255
/processorParam: ColorKeyEnabled = True
/processorParam: GenerateMipmaps = False
/processorParam: PremultiplyAlpha = True
/processorParam: ResizeToPowerOfTwo = False
/processorParam: MakeSquare = False
/processorParam: TextureFormat = Color
/build: aerial_explosion.png")]
        [InlineData(@"#----------------------------- Global Properties ----------------------------#
/outputDir:bin/$(Platform)
/intermediateDir:obj/$(Platform)
/platform:Windows
/config:
/profile:Reach
/compress:False", @"{Global Properties}
/outputDir: bin/$(Platform)
/intermediateDir: obj/$(Platform)
/platform: Windows
/config: 
/profile: Reach
/compress: False")]
        public void Section(string input, string expected)
        {
            var st = Parsers.Section.Parse(ProjectTokenizer.Create().Tokenize(input));
            st.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData(@"#----------------------------- Global Properties ----------------------------#
/outputDir:bin/$(Platform)
/intermediateDir:obj/$(Platform)
/platform:Windows
/config:
/profile:Reach
/compress:False", @"{Global Properties}
/outputDir: bin/$(Platform)
/intermediateDir: obj/$(Platform)
/platform: Windows
/config: 
/profile: Reach
/compress: False")]
        public void RegularSection(string input, string expected)
        {
            var st = Parsers.Section.Parse(ProjectTokenizer.Create().Tokenize(input));
            st.ToString().Should().Be(expected);
        }
    }
}