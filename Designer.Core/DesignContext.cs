using System;
using Designer.Domain.ViewModels;
using ReactiveUI;

namespace Designer.Core
{
    public class DesignContext : ReactiveObject, IDesignContext
    {
        private IDesignCommandsHost designCommandsHost;

        private Document document;

        public DesignContext()
        {
            MessageBus.Current.Listen<CommandsHostChanged>()
                .Subscribe(message => DesignCommandsHost = message.CommandsHost);
        }

        public IDesignCommandsHost DesignCommandsHost
        {
            get => designCommandsHost;
            set => this.RaiseAndSetIfChanged(ref designCommandsHost, value);
        }

        public Document Document
        {
            get => document;
            set => this.RaiseAndSetIfChanged(ref document, value);
        }
    }
}