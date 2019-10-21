using System.Linq;
using FluentAssertions;
using Xunit;

namespace Designer.Model.Tests
{
    public class TokenizationTests
    {
        [InlineData("#----------------------------- Global Properties ----------------------------#", new[]{ PrjToken.Hash, PrjToken.Bar, PrjToken.Text, PrjToken.Bar, PrjToken.Hash })]
        [InlineData("#----------------------------- Global Properties ----------------------------#\n", new[] { PrjToken.Hash, PrjToken.Bar, PrjToken.Text, PrjToken.Bar, PrjToken.Hash, PrjToken.WhiteSpace })]
        [InlineData("/compress:False", new[] { PrjToken.Slash, PrjToken.Text, PrjToken.Colon, PrjToken.Text })]
        [InlineData(@"/reference:..\..\..\ContentPipeline\TextureTessellation\bin\Debug\net45\VelcroPhysics.ContentPipelines.TextureTesselation.dll", new[] { PrjToken.Slash, PrjToken.Text, PrjToken.Colon, PrjToken.Text })]
        [InlineData(@"/processorParam:TextureFormat=Color", new[] { PrjToken.Slash, PrjToken.Text, PrjToken.Colon, PrjToken.Text, PrjToken.Equal, PrjToken.Text })]
        [Theory]
        public void Test(string input, PrjToken[] tokens)
        {
            var tokenizer = ProjectTokenizer.Create();
            var result = tokenizer.Tokenize(input);
            result.Select(token => token.Kind).Should().BeEquivalentTo(tokens);
        }
    }
}