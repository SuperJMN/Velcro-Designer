using System;
using System.Collections.Generic;
using System.Linq;
using Designer.Core;
using Designer.Core.Persistence;
using Designer.Plugins;
using Grace.DependencyInjection;
using Zafiro.Core;
using Zafiro.Uwp.Controls;
using Tool = Designer.Core.Tools.Tool;

namespace Designer
{
    public class Composition
    {
        private static readonly DependencyInjectionContainer Container;

        static Composition()
        {
            Container = new DependencyInjectionContainer();
            Container.Configure(registrationBlock =>
            {
                var toolType = typeof(Tool);

                registrationBlock.Export(toolType.Assembly.ExportedTypes
                        .Where(TypesThat.AreBasedOn<Tool>())
                        .Where(x => !x.IsAbstract))
                    .ByTypes(type => new[] { toolType });

                registrationBlock.Export<UwpFilePicker>().As<IFilePicker>().Lifestyle.Singleton();
                registrationBlock.Export<DesignContext>().As<IDesignContext>().Lifestyle.Singleton();
                registrationBlock.Export<PluginProvider>().As<IPluginProvider>().Lifestyle.Singleton();
                registrationBlock.Export<ViewModelFactory>().As<IViewModelFactory>().Lifestyle.Singleton();
                registrationBlock.Export<ProjectStore>().As<IProjectStore>().Lifestyle.Singleton();
            });
        }

        public static MainViewModel Root => Container.Locate<MainViewModel>();
    }
}