using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Designer.Core.Tools;
using ReactiveUI;

namespace Designer.Core
{
    public class Project : ReactiveObject
    {
        private Document selectedDocument;

        public Project(IViewModelFactory factory, IDesignContext context, IEnumerable<Tool> tools)
        {
            Tools = tools;

            var selectedObjectObs = this.WhenAnyValue(x => x.SelectedDocument);
            selectedObjectObs.Subscribe(document =>
            {
                if (document != null)
                {
                    context.Nodes = document.Graphics;
                }

                context.Selection = new List<Node>();
            });

            var selectedObjects = this.WhenAnyObservable(x => x.SelectedDocument.SelectedObjects);
            selectedObjects.Subscribe(selection => context.Selection = selection);

            AddDocument = ReactiveCommand.Create(() =>
            {
                Documents.Add(factory.CreateDocument());
            });
        }

        public ReactiveCommand<Unit, Unit> AddDocument { get; set; }

        public ObservableCollection<Document> Documents { get; private set; } = new ObservableCollection<Document>();

        public Document SelectedDocument
        {
            get => selectedDocument ?? Documents?.FirstOrDefault();
            set => this.RaiseAndSetIfChanged(ref selectedDocument, value);
        }

        public IEnumerable<Tool> Tools { get; }
    }
}