namespace Designer.Model
{
    public class WheelJoint : Node
    {
        public double MotorSpeed { get; set; }
        public Body Wheel { get; set; }
        public Point Anchor { get; set; }
        public Point Axis { get; set; }
    }
}