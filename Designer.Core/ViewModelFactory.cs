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
            return locatorService.Locate<Project>();
        }

        public Document CreateDocument()
        {
            return locatorService.Locate<Document>();
        }
    }
}