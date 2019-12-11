using System.Threading.Tasks;
using Designer.Domain.Models;
using Designer.Domain.ViewModels;
using WheelJoint = Designer.Domain.ViewModels.WheelJoint;

namespace Designer.Core.Tools
{
    public class WheelJointTool : Tool
    {
        public WheelJointTool(IDesignContext context) : base(context)
        {
        }

        protected override Task<CreationResult> Create(Rect creationArea)
        {
            var creationResult = new CreationResult(new WheelJoint()
            {
                Left = creationArea.Left,
                Top = creationArea.Top,
            });

            return Task.FromResult(creationResult);
        }
    }
}