using System;
using Designer.Core;
using Designer.Domain.ViewModels;
using Grace.DependencyInjection;
using Xunit;

namespace Designer.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
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
                HorizontalRadius = 12,
                Left = 15,
                Name = "Ellipse",
                Top = 20,
                VerticalRadius = 80,
            });

            var sut = new ProjectMapper(new DependencyInjectionContainer());
            var mapped = sut.Map(project);
        }
    }
}
