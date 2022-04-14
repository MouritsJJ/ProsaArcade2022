using static SDL2.SDL;

namespace Easter.Objects
{
    public class App
    {
        public App(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public IntPtr Window { get; set; }
        public IntPtr Renderer { get; set; }
        public IntPtr BG { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}