using static SDL2.SDL;

namespace Easter.Objects
{
    public class App
    {
        public App(int width, int height, int tileSize, int velocity, int fPS)
        {
            Width = width;
            Height = height;
            TileSize = tileSize;
            Velocity = velocity;
            FPS = fPS;
        }

        public IntPtr Window { get; set; }
        public IntPtr Renderer { get; set; }
        public IntPtr BG { get; set; }
        public List<IntPtr> Eggs { get; set; } = new List<IntPtr>();
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileSize { get; set; }
        public int Velocity { get; set; }
        public int FPS { get; set; }
        public Bunny Bunny { get; set; } = new Bunny();
    }
}