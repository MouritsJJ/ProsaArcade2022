using static SDL2.SDL;

namespace Easter.Objects
{
    public class Bunny
    {
        public SDL_Rect Pos { get; set; }
        public IntPtr West { get; set; }
        public IntPtr East { get; set; }
        public IntPtr North { get; set; }
        public IntPtr South { get; set; }
        public int VelX { get; set; }
        public int VelY { get; set; }
    }
}