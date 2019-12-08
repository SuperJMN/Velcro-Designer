namespace Designer.Domain.ViewModels
{
    public class CreationResult
    {
        public Item Node { get; }
        public bool IsSuccessful { get; set; } = true;

        public CreationResult(Item node)
        {
            Node = node;
        }

        public CreationResult()
        {
            IsSuccessful = false;
        }
    }
}