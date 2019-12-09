using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using DynamicData;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class Document : ReactiveObject
    {
        public IEnumerable<Tool> Tools { get; }
        private IEnumerable selectedItems;
        private string name;
        private readonly SourceList<Item> sourceList;
        private readonly ReadOnlyObservableCollection<Item> graphics;

        public Document(IEnumerable<Tool> tools)
        {
            Tools = tools;
            Selection = this
                .WhenAnyValue(x => x.SelectedItems)
                .Select(list => list == null ? new List<Item>() : list.Cast<Item>().ToList());

            sourceList = new SourceList<Item>();
            sourceList
                .Connect()
                .ObserveOn(SynchronizationContext.Current)
                .Bind(out graphics)
                .DisposeMany()
                .Subscribe();
        }

        public IObservable<ReactiveCommand<Unit, Unit>> AlignChanged { get; set; }

        public ReadOnlyObservableCollection<Item> Graphics => graphics;

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

        public IObservable<IList<Item>> Selection { get; }

        public void Add(IEnumerable<Item> items)
        {
            sourceList.AddRange(items);
        }

        public void Add(Item item)
        {
            sourceList.Add(item);
        }
    }
}