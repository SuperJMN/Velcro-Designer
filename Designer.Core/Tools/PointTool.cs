using System.Threading.Tasks;
using Designer.Domain.Models;
using Designer.Domain.ViewModels;
using Point = Designer.Domain.ViewModels.Point;

namespace Designer.Core.Tools
{
    public class PointTool : Tool
    {
        public PointTool(IDesignContext context) : base(context)
        {
        }

        protected override Task<CreationResult> Create(Rect creationArea, int id)
        {
            var creationResult = new CreationResult(new Point
            {
                Left = creationArea.Left,
                Top = creationArea.Top,
            });

            return Task.FromResult(creationResult);
        }
    }
}