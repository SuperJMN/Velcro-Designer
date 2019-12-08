using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public abstract class Graphic : ReactiveObject
    {
        private double left;
        private double top;

        public double Left
        {
            get => left;
            set => this.RaiseAndSetIfChanged(ref left, value);
        }

        public double Top
        {
            get => top;
            set => this.RaiseAndSetIfChanged(ref top, value);
        }
    }
}