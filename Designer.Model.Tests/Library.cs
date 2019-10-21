namespace Designer.Model.Tests
{
    public class Library
    {
        public Book[] Books { get; }

        public Library(Book[] books)
        {
            Books = books;
        }
    }
}