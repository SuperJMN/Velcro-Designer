using System.Threading.Tasks;
using Designer.Model;

namespace Designer.Core.Tools
{
    public class ImageTool : Tool
    {
        protected override Task<CreationResult> Create(Rect creationArea)
        {
            //var file = await picker.PickSingleFileAsync();
            //if (file == null)
            //{
            //    return new CreationResult();
            //}

            //var bytes = await file.ReadBytesAsync();
            
            //return new CreationResult(new Image
            //{
            //    Source = bytes,
            //});
            return Task.FromResult(new CreationResult(new CircleShape()));
        }

        public ImageTool(IDesignContext context) : base(context)
        {
        }
    }
}