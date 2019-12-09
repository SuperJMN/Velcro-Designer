using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Designer.Domain.ViewModels;
using ReactiveUI;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Designer.Parts.Editors
{
    public sealed partial class GraphicSelector : UserControl
    {
        public GraphicSelector()
        {
            this.InitializeComponent();
            this.WhenAnyValue(x => x.Document)
                .Select(x => x?.Graphics)
                .Subscribe(x => Items = x);
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(IEnumerable), typeof(GraphicSelector), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable Items
        {
            get { return (IEnumerable) GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register(
            "Document", typeof(Document), typeof(GraphicSelector), new PropertyMetadata(default(Document)));

        public Document Document
        {
            get { return (Document) GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof(Item), typeof(GraphicSelector), new PropertyMetadata(default(Item)));

        public Item SelectedItem
        {
            get { return (Item) GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
    }
}
