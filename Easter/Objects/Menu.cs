using static SDL2.SDL;
using static Easter.Core.Game;

namespace Easter.Objects
{
    public class Menu
    {
        public IntPtr MenuTitle { get; set; }
        public SDL_Rect MenuTitlePos = new SDL_Rect();
        public IntPtr MenuTitle1 { get; set; }
        public SDL_Rect MenuTitlePos1 = new SDL_Rect();

        public IntPtr SecondsTitle { get; set; }
        public SDL_Rect SecondsTitlePos = new SDL_Rect();

        public IntPtr Seconds30 { get; set; }
        public IntPtr Seconds30Standard { get; set; }
        public IntPtr Seconds30Highlight { get; set; }
        public SDL_Rect Seconds30Pos = new SDL_Rect();

        public IntPtr Seconds60 { get; set; }
        public IntPtr Seconds60Standard { get; set; }
        public IntPtr Seconds60Highlight { get; set; }
        public SDL_Rect Seconds60Pos = new SDL_Rect();

        public IntPtr Seconds120 { get; set; }
        public IntPtr Seconds120Standard { get; set; }
        public IntPtr Seconds120Highlight { get; set; }
        public SDL_Rect Seconds120Pos = new SDL_Rect();

        public bool MouseClicked { get; set; } = false;

        public void Update(ref bool running, App app)
        {
            SDL_GetMouseState(out int x, out int y);
            SDL_Rect rec = new SDL_Rect() { h = 0, w = 0, x = x, y = y };

            bool clicked = false;
            if (CollisionDetection(rec, Seconds30Pos)) 
            {
                Seconds30 = Seconds30Highlight;
                clicked = MouseClicked;
                if (clicked) app.GameTime = 30;
            } 
            else Seconds30 =  Seconds30Standard;

            if (CollisionDetection(rec, Seconds60Pos)) 
            {
                Seconds60 = Seconds60Highlight;
                clicked = MouseClicked;
                if (clicked) app.GameTime = 60;
            } 
            else Seconds60 =  Seconds60Standard;

            if (CollisionDetection(rec, Seconds120Pos)) 
            {
                Seconds120 = Seconds120Highlight;
                clicked = MouseClicked;
                if (clicked) app.GameTime = 120;
            } 
            else Seconds120 =  Seconds120Standard;

            if (clicked) running = false;
        }

        public void Clean()
        {
            SDL_DestroyTexture(SecondsTitle);

            SDL_DestroyTexture(Seconds30);
            SDL_DestroyTexture(Seconds30Standard);
            SDL_DestroyTexture(Seconds30Highlight);

            SDL_DestroyTexture(Seconds60);
            SDL_DestroyTexture(Seconds60Standard);
            SDL_DestroyTexture(Seconds60Highlight);

            SDL_DestroyTexture(Seconds120);
            SDL_DestroyTexture(Seconds120Standard);
            SDL_DestroyTexture(Seconds120Highlight);
        }
    }
}