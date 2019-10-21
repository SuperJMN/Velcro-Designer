using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace Designer.Model.Tests
{
    public static class ProjectTokenizer
    {
        public static Tokenizer<PrjToken> Create()
        {
            return new TokenizerBuilder<PrjToken>()
                .Match(Character.EqualTo('#'), PrjToken.Hash)
                .Match(Character.EqualTo('/'), PrjToken.Slash)
                .Match(Span.Regex(@"-{2,}"), PrjToken.Bar)
                .Match(Character.EqualTo('-'), PrjToken.Dash)
                .Match(Character.EqualTo(':'), PrjToken.Colon)
                .Match(Character.EqualTo('='), PrjToken.Equal)
                .Match(Span.EqualTo("begin"), PrjToken.Begin)
                .Match(Span.Regex("[^\r\n#:=-]*"), PrjToken.Text)
                .Match(Span.WhiteSpace, PrjToken.WhiteSpace)
                .Build();
        }
    }
}