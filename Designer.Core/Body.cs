using ReactiveUI;

namespace Designer.Core
{
    public abstract class Body : Node
    {
        private double density;
        private string texture;

        public string Texture
        {
            get => texture;
            set => this.RaiseAndSetIfChanged(ref texture, value);
        }

        public double Density
        {
            get => density;
            set => this.RaiseAndSetIfChanged(ref density, value);
        }
    }
}