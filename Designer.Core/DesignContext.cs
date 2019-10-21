using System;
using System.Collections.Generic;
using ReactiveUI;

namespace Designer.Core
{
    public class DesignContext : ReactiveObject, IDesignContext
    {
        private ICollection<Node> nodes;
        private ICollection<Node> selection;

        public ICollection<Node> Nodes
        {
            get => nodes;
            set => this.RaiseAndSetIfChanged(ref nodes, value);
        }

        public ICollection<Node> Selection
        {
            get => selection;
            set => this.RaiseAndSetIfChanged(ref selection, value);
        }

        public IObservable<bool> SelectionObs { get; set; }
    }
}