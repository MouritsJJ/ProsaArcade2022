using static SDL2.SDL;

namespace Easter.Objects
{
    public class Bump
    {
        public SDL_Rect Pos = new SDL_Rect();
        public IntPtr Texture { get; set; }
        public IntPtr Egg { get; set; }
        public int Points { get; set; }

        public void Clean()
        {
            SDL_DestroyTexture(Texture);
            SDL_DestroyTexture(Egg);
        }
    }
}