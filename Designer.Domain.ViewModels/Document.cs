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
        private ReadOnlyObservableCollection<Item> itemsCollection;
        private SourceList<Item> items;

        public Document(IEnumerable<Tool> tools)
        {
            Tools = tools;
            Selection = this
                .WhenAnyValue(x => x.SelectedItems)
                .Select(list => list == null ? new List<Item>() : list.Cast<Item>().ToList());

            items = new SourceList<Item>();
        }

        public IObservable<ReactiveCommand<Unit, Unit>> AlignChanged { get; set; }

        public ReadOnlyObservableCollection<Item> ItemsCollection => itemsCollection;

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

        public SourceList<Item> Items
        {
            get => items;
            private set
            {
                items = value;
                items
                    .Connect()
                    .ObserveOn(SynchronizationContext.Current)
                    .Bind(out itemsCollection)
                    .DisposeMany()
                    .Subscribe();
            }
        }
    }
}