using static SDL2.SDL;
using static SDL2.SDL_mixer;
using static Easter.Core.Game;

namespace Easter.Objects
{
    public class EndMenu
    {
        public IntPtr ScoreTitle { get; set; }
        public SDL_Rect ScoreTitlePos = new SDL_Rect();

        public IntPtr Score { get; set; }
        public SDL_Rect ScorePos = new SDL_Rect();
        public IntPtr ScoreP { get; set; }
        public SDL_Rect ScorePPos = new SDL_Rect();

        public List<SDL_Rect> EggsPos = new List<SDL_Rect>();
        public List<IntPtr> EggsScore { get; set; } = new List<IntPtr>();
        public List<SDL_Rect> EggsScorePos = new List<SDL_Rect>();

        public IntPtr NewGame { get; set; }
        public IntPtr NewGameStandard { get; set; }
        public IntPtr NewGameHighlight { get; set; }
        public SDL_Rect NewGamePos = new SDL_Rect();


        public bool MouseClicked { get; set; } = false;
        public bool Loaded { get; set; } = false;

        public void Update(ref bool running, App app)
        {
            SDL_GetMouseState(out int x, out int y);
            SDL_Rect rec = new SDL_Rect() { h = 0, w = 0, x = x, y = y };

            bool clicked = false;
            if (CollisionDetection(rec, NewGamePos)) 
            {
                NewGame = NewGameHighlight;
                clicked = MouseClicked;
            } 
            else NewGame = NewGameStandard;

            if (clicked)
            {
                running = false;
                Mix_PlayChannel(-1, app.Menu_Sound, 0);
                MouseClicked = false;
            }
        }

        public void Clean()
        {
            SDL_DestroyTexture(ScoreTitle);
            SDL_DestroyTexture(Score);
            SDL_DestroyTexture(ScoreP);

            foreach (var p in EggsScore) SDL_DestroyTexture(p);

            SDL_DestroyTexture(NewGame);
            SDL_DestroyTexture(NewGameStandard);
            SDL_DestroyTexture(NewGameHighlight);
        }

        public void Reset()
        {
            Loaded = false;
            foreach(var egg in EggsScore) SDL_DestroyTexture(egg);
            EggsScore.Clear();
        }
    }
}