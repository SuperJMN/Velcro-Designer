using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace Designer.Model.Tests
{
    public class MyParsers
    {
        public static readonly TokenListParser<PrjToken, string> Text = from text in Token.EqualTo(PrjToken.Text)
            select text.ToStringValue();

        public static readonly TokenListParser<PrjToken, Token<PrjToken>> Whitespace = from text in Token.EqualTo(PrjToken.WhiteSpace)
            select text;

        public static readonly TokenListParser<PrjToken, string> Title =
            from hash in Token.EqualTo(PrjToken.Hash)
            from text in Text
            from wh in Whitespace 
            select text;

        public static readonly TokenListParser<PrjToken, string> Chapter =
            from chapterName in Text
            from wh in Whitespace
            select chapterName;

        public static readonly TokenListParser<PrjToken, Book> Book =
            from title in Title
            from chapters in Chapter.Many()
            from wh in Whitespace 
            select new Book(title, chapters);

        public static readonly TokenListParser<PrjToken, Library> Library =
            from books in Book.Many()
            select new Library(books);
    }
}