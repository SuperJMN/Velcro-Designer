using System.Collections.Generic;
using Designer.Domain.Models;

namespace Designer.Tests
{
    public static class ProjectBuilder
    {
        public static Project Build()
        {
            var firstShape = new EllipseShape();
            var secondShape = new EllipseShape();
            return new Project
            {
                Documents = new List<Document>
                {
                    new Document
                    {
                        Items = new List<Item>
                        {
                            firstShape,
                            secondShape ,
                            new WheelJoint
                            {
                                FirstBody = firstShape,
                                SecondBody = secondShape
                            }
                        }
                    }
                }
            };
        }
    }
}