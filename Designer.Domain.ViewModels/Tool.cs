using System.Threading.Tasks;
using System.Windows.Input;
using Designer.Domain.Models;
using DynamicData;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public abstract class Tool
    {
        protected Tool(IDesignContext context)
        {
            CreateCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var creationResult = await Create(CreationArea, context.Document.ItemsCollection.Count + 1);
                if (creationResult.IsSuccessful)
                {
                    context.Document.Items.Add(creationResult.Node);
                }
            });
        }

        public Rect CreationArea { get; } = new Rect(0, 0, 200, 100);

        public ICommand CreateCommand { get; }        

        protected abstract Task<CreationResult> Create(Rect creationArea, int id);
    }
}