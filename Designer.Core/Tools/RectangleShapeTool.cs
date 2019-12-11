using System.Threading.Tasks;
using Designer.Domain.Models;
using Designer.Domain.ViewModels;

namespace Designer.Core.Tools
{
    public class RectangleShapeTool : Tool
    {
        public RectangleShapeTool(IDesignContext context) : base(context)
        {
        }

        protected override Task<CreationResult> Create(Rect creationArea)
        {
            var creationResult = new CreationResult(new RectangleShape()

            {
                Left = creationArea.Left,
                Width = creationArea.Width,
                Top = creationArea.Top,
                Height = creationArea.Height,
            });

            return Task.FromResult(creationResult);
        }
    }
}