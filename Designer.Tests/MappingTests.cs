using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Designer.Core;
using Designer.Core.Mapper;
using Designer.Core.Persistence;
using Designer.Core.Tools;
using Designer.Domain.ViewModels;
using Grace.DependencyInjection;
using Grace.DependencyInjection.Impl;
using Grace.DependencyInjection.Impl.CompiledStrategies;
using Grace.DependencyInjection.Impl.FactoryStrategies;
using Grace.DependencyInjection.Impl.KnownTypeStrategies;
using Xunit;
using Zafiro.Core;
using Zafiro.Core.Files;

namespace Designer.Tests
{
    public class MappingTests
    {
        [Fact]
        public void To_model()
        {
            var document = new Document(null);

            var project = new Project(null, null, null)
            {
                Documents =
                {
                    document
                },
            };

            document.Add(new EllipseShape()
            {
                Id = 1,
                HorizontalRadius = 12,
                Left = 15,
                Name = "Ellipse",
                Top = 20,
                VerticalRadius = 80,
            });

            var first = new EllipseShape {Id = 2,};
            var second = new EllipseShape {Id = 3};

            document.Add(new WheelJoint()
            {
                SecondBody = first,
                FirstBody = second,
                Name = "WheelJoint",
            });

            document.Add(first);
            document.Add(second);

            var sut = new ProjectMapper(new DependencyInjectionContainer());
            var mapped = sut.Map(project);
        }

        [Fact]
        public void From_model()
        {
            var project = ProjectBuilder.Build();

            var container = new DependencyInjectionContainer();
            container.Configure(registrationBlock =>
            {
                var toolType = typeof(Tool);
                var assembly = typeof(EllipseShapeTool).Assembly;

                registrationBlock.Export(assembly.ExportedTypes
                        .Where(TypesThat.AreBasedOn<Tool>())
                        .Where(x => !x.IsAbstract))
                    .ByTypes(type => new[] { toolType });

                registrationBlock.Export<DesignContext>().As<IDesignContext>().Lifestyle.Singleton();
                registrationBlock.Export<ViewModelFactory>().As<IViewModelFactory>().Lifestyle.Singleton();
                registrationBlock.Export<ProjectStore>().As<IProjectStore>().Lifestyle.Singleton();
                registrationBlock.Export<ProjectMapper>().As<IProjectMapper>().Lifestyle.Singleton();
                registrationBlock.Export<ImportExtensionsViewModel>().Lifestyle.Singleton();
                registrationBlock.Export<MainViewModel>().Lifestyle.Singleton();
                registrationBlock.Export<CodeExporter>().As<IExporter>().Lifestyle.Singleton();

            });

            var sut = new ProjectMapper(container);
            var mapped = sut.Map(project);
        }
    }
}
