namespace Designer.Core
{
    public interface IViewModelFactory
    {
        Project CreateProject();
        Document CreateDocument();
    }
}