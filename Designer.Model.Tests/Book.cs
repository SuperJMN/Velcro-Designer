namespace Designer.Model.Tests
{
    public class Book
    {
        public string Title { get; }
        public string[] Chapters { get; }

        public Book(string title, string[] chapters)
        {
            Title = title;
            Chapters = chapters;
        }
    }
}