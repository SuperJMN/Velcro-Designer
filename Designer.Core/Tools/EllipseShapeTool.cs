using System.Threading.Tasks;
using Designer.Domain.ViewModels;
using Rect = Designer.Domain.Models.Rect;

namespace Designer.Core.Tools
{
    public class EllipseShapeTool : Tool
    {
        public EllipseShapeTool(IDesignContext context) : base(context)
        {
        }

        protected override Task<CreationResult> Create(Rect creationArea)
        {
            var creationResult = new CreationResult(new EllipseShape
            {
                Left = creationArea.Left,
                HorizontalRadius = creationArea.Width,
                Top = creationArea.Top,
                VerticalRadius = creationArea.Height,
            });

            return Task.FromResult(creationResult);
        }
    }
}