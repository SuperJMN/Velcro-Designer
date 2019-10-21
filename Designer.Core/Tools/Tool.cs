using System.Threading.Tasks;
using System.Windows.Input;
using Designer.Model;
using ReactiveUI;

namespace Designer.Core.Tools
{
    public abstract class Tool
    {
        protected Tool(IDesignContext context)
        {
            CreateCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var creationResult = await Create(CreationArea);
                if (creationResult.IsSuccessful)
                {
                    context.Nodes.Add(creationResult.Node);
                }
            });
        }

        public Rect CreationArea { get; } = new Rect(0, 0, 200, 100);

        public ICommand CreateCommand { get; }        

        protected abstract Task<CreationResult> Create(Rect creationArea);
    }
}