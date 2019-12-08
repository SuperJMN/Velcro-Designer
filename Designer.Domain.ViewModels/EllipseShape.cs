using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class EllipseShape : Item
    {
        private double horizontalRadius;

        public double HorizontalRadius
        {
            get => horizontalRadius;
            set => this.RaiseAndSetIfChanged(ref horizontalRadius, value);
        }

        private double verticalRadius;

        public double VerticalRadius
        {
            get => verticalRadius;
            set => this.RaiseAndSetIfChanged(ref verticalRadius, value);
        }
    }
}