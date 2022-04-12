using static SDL2.SDL;

namespace Easter.Core
{
    public static class SDLHelper
    {
        public static void Setup(App app) 
        {
            // Initilizes SDL
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"There was an issue initializing SDL. {SDL_GetError()}");
            }

            // Create a new window given a title, size, and passes it a flag indicating it should be shown.
            app.Window = SDL_CreateWindow(
                "SDL .NET 6 Tutorial",
                SDL_WINDOWPOS_UNDEFINED, 
                SDL_WINDOWPOS_UNDEFINED, 
                640, 
                480, 
                SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (app.Window == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the window. {SDL_GetError()}");
            }

            // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
            app.Renderer = SDL_CreateRenderer(
                app.Window,
                -1,
                SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (app.Renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL_GetError()}");
            }
        }
    
        public static void PollEvents(ref bool running)
        {
            // Check to see if there are any events and continue to do so until the queue is empty.
            while (SDL_PollEvent(out SDL_Event e) == 1)
            {
                switch (e.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        running = false;
                        break;
                }
            }
        }
    
        public static void Render(App app)
        {
            // Sets the color that the screen will be cleared with.
            SDL_SetRenderDrawColor(app.Renderer, 135, 206, 235, 255);

            // Clears the current render surface.
            SDL_RenderClear(app.Renderer);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL_RenderPresent(app.Renderer);
        }

        public static void CleanUp(App app)
        {
            SDL_DestroyRenderer(app.Renderer);
            SDL_DestroyWindow(app.Window);
            SDL_Quit();
        }
    }
}