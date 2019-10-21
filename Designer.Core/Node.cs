using ReactiveUI;

namespace Designer.Core
{
    public abstract class Node : ReactiveObject
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

        public abstract double Width { get; }

        public abstract double Height { get; }
    }

    public class WheelJoint : Node
    {
        public double MotorSpeed { get; set; }
        public Body Wheel { get; set; }
        public Point Anchor { get; set; }
        public Point Axis { get; set; }
        public override double Width => 10;
        public override double Height => 10;
    }

    public class Point : Node
    {
        public override double Width => 10;
        public override double Height => 10;
    }
}