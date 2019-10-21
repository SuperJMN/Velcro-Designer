namespace Designer.Model
{
    public abstract class Body : Node
    {

        public string Texture
        {
            get; set;
        }

        public double Density
        {
            get; set;
        }
    }
}