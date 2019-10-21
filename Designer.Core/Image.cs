using Zafiro.Core;

namespace Designer.Core
{
    public class Image : Node
    {
        [Hidden]
        public byte[] Source { get; set; }

        public override double Width => 10;
        public override double Height => 10;
    }
}