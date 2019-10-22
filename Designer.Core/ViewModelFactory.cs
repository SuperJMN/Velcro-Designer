using Grace.DependencyInjection;

namespace Designer.Core
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly ILocatorService locatorService;

        public ViewModelFactory(ILocatorService locatorService)
        {
            this.locatorService = locatorService;
        }

        public Project CreateProject()
        {
            var project = locatorService.Locate<Project>();
            project.Documents.Add(CreateDocument());
            return project;
        }

        public Document CreateDocument()
        {
            var document = locatorService.Locate<Document>();
            document.Name = "New document";
            return document;
        }
    }
}