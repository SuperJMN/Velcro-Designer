using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace Designer.Model.Tests
{
    public class Parsers
    {
        public static readonly TokenListParser<PrjToken, string> Text = from text in Token.EqualTo(PrjToken.Text)
                                                                        select text.ToStringValue();

        public static readonly TokenListParser<PrjToken, Token<PrjToken>> Whitespace = from text in Token.EqualTo(PrjToken.WhiteSpace)
                                                                                       select text;

        public static readonly TokenListParser<PrjToken, string> Header = from h1 in Token.EqualTo(PrjToken.Hash)
                                                                          from b1 in Token.EqualTo(PrjToken.Bar)
                                                                          from text in Text
                                                                          from b2 in Token.EqualTo(PrjToken.Bar)
                                                                          from h2 in Token.EqualTo(PrjToken.Hash)
                                                                          select text.Trim();

        public static readonly TokenListParser<PrjToken, Statement> RegularStatement =
            from b1 in Token.EqualTo(PrjToken.Slash)
            from text in Text
            from b2 in Token.EqualTo(PrjToken.Colon)
            from value in Text.OptionalOrDefault()
            select (Statement)new RegularStatement(text, value);

        public static readonly TokenListParser<PrjToken, Statement> AssignmentStatement =
            from b1 in Token.EqualTo(PrjToken.Slash)
            from verb in Text
            from b2 in Token.EqualTo(PrjToken.Colon)
            from property in Text
            from eq in Token.EqualTo(PrjToken.Equal)
            from value in Text
            select (Statement)new AssignmentStatement(verb, property, value);

        public static readonly TokenListParser<PrjToken, Statement> Statement = AssignmentStatement.Try().Or(RegularStatement);

        public static readonly TokenListParser<PrjToken, Statement[]> Statements = Statement.Try().AtLeastOnceDelimitedBy(Whitespace);

        public static readonly TokenListParser<PrjToken, string> Begin =
            from hash in Token.EqualTo(PrjToken.Hash)
            from begin in Token.EqualTo(PrjToken.Begin)
            from text in Text
            select text.Trim();


        public static readonly TokenListParser<PrjToken, Resource> ResourceEntry =
            from begin in Begin
            from wh in Whitespace
            from statements in Statements
            select new Resource(begin, statements);

        public static readonly TokenListParser<PrjToken, Content> StatementContent = 
            Statements.Select(n => (Content)new StatementContent(n));

        public static readonly TokenListParser<PrjToken, Content> EmptyContent = from s in Whitespace
                                                                                 select (Content)new EmptyContent();

        public static readonly TokenListParser<PrjToken, Section> Section =
            from h in Header
            from wh in Whitespace
            from content in Content
            select new Section(h, content);

        private static TokenListParser<PrjToken, Content> Content => StatementContent.Or(ResourcesContent).Or(EmptyContent);

        public static readonly TokenListParser<PrjToken, Content> ResourcesContent =
            ResourceEntry.AtLeastOnceDelimitedBy(Whitespace).Select(n => (Content) new ResourceContent(n));

        public static readonly TokenListParser<PrjToken, Section[]> Sections = Section.ManyDelimitedBy(Whitespace);

        public static readonly TokenListParser<PrjToken, ProjectFile> PrjParser =
            from sections in Sections
            select new ProjectFile(sections);
    }
}