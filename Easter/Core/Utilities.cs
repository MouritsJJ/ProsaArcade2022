using static SDL2.SDL;
using static SDL2.SDL_image;
using Easter.Objects;

namespace Easter.Core
{
    public static class Utilities
    {
        public static void Setup(App app) 
        {
            Init(SDL_INIT_VIDEO);
            app.Window = CreateWindow(app.Width, app.Height);
            app.Renderer = CreateRenderer(app.Window);
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
            // SDL_SetRenderDrawColor(app.Renderer, 135, 206, 235, 255);

            // Clears the current render surface.
            SDL_RenderClear(app.Renderer);
            SDL_RenderCopy(app.Renderer, app.BG, IntPtr.Zero, IntPtr.Zero);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL_RenderPresent(app.Renderer);
        }

        public static void CleanUp(App app)
        {
            SDL_DestroyRenderer(app.Renderer);
            SDL_DestroyWindow(app.Window);
            SDL_Quit();
        }

        public static IntPtr CreateWindow(int w, int h)
        {
            // Create a new window given a title, size, and passes it a flag indicating it should be shown.
            var window = SDL_CreateWindow(
                "Easter Game",
                SDL_WINDOWPOS_CENTERED, 
                SDL_WINDOWPOS_CENTERED, 
                w, 
                h, 
                SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (window == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the window. {SDL_GetError()}");
            }
            
            return window;
        }

        public static IntPtr CreateRenderer(IntPtr window)
        {
            // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
            var renderer = SDL_CreateRenderer(
                window,
                -1,
                SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL_GetError()}");
            }

            return renderer;
        }
    
        public static void Init(uint flags)
        {
            // Initilizes SDL
            if (SDL_Init(flags) < 0)
            {
                Console.WriteLine($"There was an issue initializing SDL. {SDL_GetError()}");
            }
        }
    
        public static IntPtr CreateImgSurface(string img)
        {
            IntPtr surface = IMG_Load(img);
            if (surface == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the surface. {SDL_GetError()}");
            }
            return surface;
        }
    
        public static IntPtr CreateTexture(IntPtr renderer, IntPtr surface)
        {
            IntPtr texture = SDL_CreateTextureFromSurface(renderer, surface);
            SDL_FreeSurface(surface);
            if (texture == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the texture. {SDL_GetError()}");
            }
            return texture;
        }
    }
}