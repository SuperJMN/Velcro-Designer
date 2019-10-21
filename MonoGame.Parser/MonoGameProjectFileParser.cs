using Superpower;

namespace Designer.Model.Tests
{
    public class MonoGameProjectFileParser : IMonoGameProjectFileParser
    {
        public ProjectFile Parse(string str)
        {
            var tokenizer = ProjectTokenizer.Create();
            var result =  Parsers.PrjParser.Parse(tokenizer.Tokenize(str.Trim()));
            return result;
        }
    }
}