using Superpower;
using Xunit;

namespace Designer.Model.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void Test()
        {
            var input =
@"#Book 1
Chapter 1
Chapter 2
#Book 2
Chapter 1
Chapter 2
Chapter 3";

            var library = MyParsers.Library.Parse(ProjectTokenizer.Create().Tokenize(input));
        }
    }
}