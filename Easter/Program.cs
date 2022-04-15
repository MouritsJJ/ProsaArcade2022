using static SDL2.SDL;
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
            bool running = true;
            App app = new App(width: 1280, height: 960, tileSize: 40, 
                velocity: 8, fPS: 60, maxBumps: 15);
            Setup(app);
            LoadResources(app);

            uint tick, wait;
            while (running)
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