using static SDL2.SDL;
using static SDL2.SDL_mixer;
using static Easter.Core.Utilities;
using static Easter.Core.StartFunc;
using static Easter.Core.Game;
using Easter.Objects;

namespace Easter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setup of game
            bool running = true;
            App app = new App(width: 1280, height: 960, tileSize: 40, 
                velocity: 8, fPS: 60, maxBumps: 15);
            Menu menu = new Menu();
            Setup(app);
            LoadResources(app, menu);

            // Start music
            Mix_PlayMusic(app.BG_Music, -1);
            Mix_VolumeMusic(20);

            // Choose gamemode
            GameMode(ref running, app, menu);

            // Main game loop
            uint tick, wait;
            while (running && app.Seconds < app.GameTime)
            {
                tick = SDL_GetTicks();
                if (++app.Frames == app.FPS) { app.Seconds++; app.Frames = 0; }

                PollEvents(ref running, app);
                UpdateApp(app);
                Render(app);

                wait = (uint)(1000 / app.FPS) - (SDL_GetTicks() - tick);
                SDL_Delay((uint)Math.Min(1000 / app.FPS, wait));
            }

            CleanUp(app);
        }
        
    }
}