using Designer.Core.Mapper;
using Designer.Domain.ViewModels;
using DynamicData;
using Grace.DependencyInjection;
using Xunit;

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
                }
            };

            document.Items.Add(new EllipseShape
            {
                HorizontalRadius = 12,
                Left = 15,
                Name = "Ellipse",
                Top = 20,
                VerticalRadius = 80
            });

            var first = new EllipseShape();
            var second = new EllipseShape();

            document.Items.Add(new WheelJoint
            {
                SecondBody = first,
                FirstBody = second,
                Name = "WheelJoint"
            });

            document.Items.Add(first);
            document.Items.Add(second);

            var sut = new ProjectMapper(new DependencyInjectionContainer());
            var mapped = sut.Map(project);
        }

        [Fact]
        public void From_model()
        {
            var project = ProjectBuilder.Build();

            var sut = new ProjectMapper(Container.Current);
            var mapped = sut.Map(project);
        }
    }
}
