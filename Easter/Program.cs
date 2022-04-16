using static SDL2.SDL;
using static SDL2.SDL_mixer;
using static Easter.Core.Utilities;
using static Easter.Core.StartFunc;
using static Easter.Core.EndFunc;
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
            EndMenu endMenu = new EndMenu();
            Setup(app);
            LoadResources(app, menu, endMenu);

            // Start music
            Mix_PlayMusic(app.BG_Music, -1);
            Mix_VolumeMusic(20);

            // Continous game loop
            while (running)
            {
                // Choose gamemode
                GameMode(ref running, app, menu);

                // Main game loop
                GameLoop(ref running, app, menu);

                // End screen
                EndScreen(ref running, app, endMenu);
            }

            CleanUp(app, menu, endMenu);
        }
        
    }
}