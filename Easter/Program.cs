using static SDL2.SDL;
using static Easter.Core.Utilities;
using static Easter.Core.StartFunc;
using Easter.Objects;

namespace Easter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool running = true;
            App app = new App(1280, 960, 40, 8, 60);
            Setup(app);
            LoadResources(app);

            uint tick, wait;
            while (running)
            {
                tick = SDL_GetTicks();

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