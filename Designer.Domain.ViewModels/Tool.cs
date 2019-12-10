using System.Threading.Tasks;
using System.Windows.Input;
using Designer.Domain.Models;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public abstract class Tool
    {
        protected Tool(IDesignContext context)
        {
            CreateCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var creationResult = await Create(CreationArea, context.Document.Graphics.Count + 1);
                if (creationResult.IsSuccessful)
                {
                    context.Document.Add(creationResult.Node);
                }
            });
        }

        public Rect CreationArea { get; } = new Rect(0, 0, 200, 100);

        public ICommand CreateCommand { get; }        

        protected abstract Task<CreationResult> Create(Rect creationArea, int id);
    }
}