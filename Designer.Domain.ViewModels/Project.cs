using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;

namespace Designer.Domain.ViewModels
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
                    context.Document = document;
                }
            });

            AddDocument = ReactiveCommand.Create(() =>
            {
                var item = factory.CreateDocument();
                item.Name = "New document";
                Documents.Add(item);
            });
        }

        public ReactiveCommand<Unit, Unit> AddDocument { get; set; }

        public ObservableCollection<Document> Documents { get; } = new ObservableCollection<Document>();

        public Document SelectedDocument
        {
            get => selectedDocument;
            set => this.RaiseAndSetIfChanged(ref selectedDocument, value);
        }

        public IEnumerable<Tool> Tools { get; }
    }
}