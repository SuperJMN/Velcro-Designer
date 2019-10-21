using System.Threading.Tasks;
using Designer.Model;

namespace Designer.Core.Tools
{
    public class CircleShapeTool : Tool
    {
        protected override async Task<CreationResult> Create(Rect creationArea)
        {
            var creationResult = new CreationResult(new CircleShape
            {
                Left = creationArea.Left,
                Top = creationArea.Top,
                Radius = 100,
            });

            return creationResult;
        }


        public CircleShapeTool(IDesignContext context) : base(context)
        {
        }
    }
}