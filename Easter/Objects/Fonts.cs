using static SDL2.SDL;
using static Easter.Core.Utilities;

namespace Easter.Objects
{
    public static class Fonts
    {
        public static IntPtr SmallFont = OpenFont(40);
        public static IntPtr BigFont = OpenFont(120);
    }
}