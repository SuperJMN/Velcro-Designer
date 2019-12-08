using System;
using System.Collections.Generic;
using Designer.Domain.ViewModels;
using ReactiveUI;

namespace Designer.Core
{
    public class DesignContext : ReactiveObject, IDesignContext
    {
        private ICollection<Item> nodes;
        private ICollection<Item> selection;
        private IDesignCommandsHost designCommandsHost;

        public DesignContext()
        {
            MessageBus.Current.Listen<CommandsHostChanged>().Subscribe(message => DesignCommandsHost = message.CommandsHost);
        }

        public ICollection<Item> Nodes
        {
            get => nodes;
            set => this.RaiseAndSetIfChanged(ref nodes, value);
        }

        public ICollection<Item> Selection
        {
            get => selection;
            set => this.RaiseAndSetIfChanged(ref selection, value);
        }

        public IDesignCommandsHost DesignCommandsHost
        {
            get => designCommandsHost;
            set => this.RaiseAndSetIfChanged(ref designCommandsHost, value);
        }
        
        public IObservable<bool> SelectionObs { get; set; }
    }
}