namespace Designer.Domain.Models
{
    public class WheelJoint : Item
    {
        public Body FirstBody { get; set; }
        public Body SecondBody { get; set; }
        public Point Anchor { get; set; }
        public Point Axis { get; set; }
    }
}