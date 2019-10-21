namespace Designer.Core.Tools
{
    public class CreationResult
    {
        public Node Node { get; }
        public bool IsSuccessful { get; } = true;

        public CreationResult(Node node)
        {
            Node = node;
        }

        public CreationResult()
        {
            IsSuccessful = false;
        }
    }
}