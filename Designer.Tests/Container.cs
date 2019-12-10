using System.Linq;
using Designer.Core;
using Designer.Core.Mapper;
using Designer.Core.Persistence;
using Designer.Core.Tools;
using Designer.Domain.ViewModels;
using Grace.DependencyInjection;

namespace Designer.Tests
{
    public static class Container
    {
        public static DependencyInjectionContainer Current
        {
            get
            {
                var container = new DependencyInjectionContainer();
                container.Configure(registrationBlock =>
                {
                    var toolType = typeof(Tool);
                    var assembly = typeof(EllipseShapeTool).Assembly;

                    registrationBlock.Export(assembly.ExportedTypes
                            .Where(TypesThat.AreBasedOn<Tool>())
                            .Where(x => !x.IsAbstract))
                        .ByTypes(type => new[] {toolType});

                    registrationBlock.Export<DesignContext>().As<IDesignContext>().Lifestyle.Singleton();
                    registrationBlock.Export<ViewModelFactory>().As<IViewModelFactory>().Lifestyle.Singleton();
                    registrationBlock.Export<ProjectStore>().As<IProjectStore>().Lifestyle.Singleton();
                    registrationBlock.Export<ProjectMapper>().As<IProjectMapper>().Lifestyle.Singleton();
                    registrationBlock.Export<ImportExtensionsViewModel>().Lifestyle.Singleton();
                    registrationBlock.Export<MainViewModel>().Lifestyle.Singleton();
                    registrationBlock.Export<CodeExporter>().As<IExporter>().Lifestyle.Singleton();
                });

                return container;
            }
        }
    }
}