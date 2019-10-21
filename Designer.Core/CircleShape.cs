using ReactiveUI;

namespace Designer.Core
{
    public class CircleShape : Body
    {
        private double radius;

        public double Radius
        {
            get => radius;
            set => this.RaiseAndSetIfChanged(ref radius, value);
        }

        public override double Width => radius;
        public override double Height => radius;
    }
}