using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Designer.Model.Tests;
using ReactiveUI;
using Zafiro.Core;

namespace Designer.Core
{
    public class Document : ReactiveObject
    {
        private IEnumerable selectedItems;
        private ProjectFile projectFile;
        private string name;

        public Document(IFilePicker filePicker)
        {
            SelectedObjects = this
                .WhenAnyValue(x => x.SelectedItems)
                .Select(list => list == null ? new List<Node>() : list.Cast<Node>().ToList());

            LoadResources = ReactiveCommand.CreateFromObservable(() =>
            {
                var obs = filePicker.Pick("Open", new[] { ".mgcb" })
                    .SelectMany(async x =>
                    {
                        var parser = new MonoGameProjectFileParser();
                        using (var p = await x.OpenForRead())
                        {
                            var input = await p.ReadToEnd();
                            return parser.Parse(input);
                        }
                    });
                return obs;
            });
        }

        public ReactiveCommand<Unit, ProjectFile> LoadResources { get; }

        public ObservableCollection<Node> Graphics { get; set; } = new ObservableCollection<Node>();

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public IEnumerable SelectedItems
        {
            get => selectedItems;
            set => this.RaiseAndSetIfChanged(ref selectedItems, value);
        }

        public ProjectFile ProjectFile
        {
            get => projectFile;
            set => this.RaiseAndSetIfChanged(ref projectFile, value);
        }

        public IObservable<IList<Node>> SelectedObjects { get; }
    }
}