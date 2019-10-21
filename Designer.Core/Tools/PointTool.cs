using System.Threading.Tasks;
using Designer.Model;

namespace Designer.Core.Tools
{
    public class PointTool : Tool
    {
        protected override Task<CreationResult> Create(Rect creationArea)
        {
            var creationResult = new CreationResult(new Point
            {
                Left = creationArea.Left,
                Top = creationArea.Top,
            });

            return Task.FromResult(creationResult);
        }

        public PointTool(IDesignContext context) : base(context)
        {
        }
    }
}