using static SDL2.SDL;
using static Easter.Core.Utilities;

namespace Easter.Objects
{
    public class Bunny
    {
        public SDL_Rect Pos = new SDL_Rect();
        public IntPtr Texture { get; set; }
        public IntPtr West { get; set; }
        public IntPtr East { get; set; }
        public IntPtr North { get; set; }
        public IntPtr South { get; set; }
        public int VelX { get; set; }
        public int VelY { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Right { get; set; }
        public bool Left { get; set; }

        public void UpdatePos(App app)
        {
            if (Up)     VelY = -app.Velocity;
            if (Down)   VelY = app.Velocity;
            if (Right)  VelX = app.Velocity;
            if (Left)   VelX = -app.Velocity;

            Pos.x += VelX;
            Pos.y += VelY;
            if (VelX > 0) Texture = East;
            if (VelX < 0) Texture = West;
            if (VelY > 0) Texture = South;
            if (VelY < 0) Texture = North;
            BlockingBorders(app, ref Pos);
        }
    }
}