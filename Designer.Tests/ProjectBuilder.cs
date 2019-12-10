using System.Collections.Generic;
using Designer.Domain.Models;

namespace Designer.Tests
{
    public static class ProjectBuilder
    {
        public static Project Build()
        {
            var firstShape = new EllipseShape() { Id = 1, };
            var secondShape = new EllipseShape() { Id = 2, };
            return new Project()
            {
                Documents = new List<Document>()
                {
                    new Document()
                    {
                        Items = new List<Item>()
                        {
                            firstShape,
                            secondShape ,
                            new WheelJoint()
                            {
                                FirstBody = firstShape,
                                SecondBody = secondShape,
                            }
                        }
                    }
                }
            };
        }
    }
}