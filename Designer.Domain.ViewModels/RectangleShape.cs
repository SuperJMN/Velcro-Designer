using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class RectangleShape : Item
    {
        private double width;
        private double height;

        public double Width
        {
            get => width;
            set => this.RaiseAndSetIfChanged(ref width, value);
        }

        public double Height
        {
            get => height;
            set => this.RaiseAndSetIfChanged(ref height, value);
        }
    }
}