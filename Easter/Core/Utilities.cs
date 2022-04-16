using static SDL2.SDL;
using static SDL2.SDL_image;
using static SDL2.SDL_ttf;
using Easter.Objects;

namespace Easter.Core
{
    public static class Utilities
    {
        public static void Setup(App app) 
        {
            Init(SDL_INIT_VIDEO | SDL_INIT_AUDIO);
            app.Window = CreateWindow(app.Width, app.Height);
            app.Renderer = CreateRenderer(app.Window);
        }
    
        public static void CleanUp(App app, Menu menu, EndMenu endMenu)
        {
            app.Clean();
            menu.Clean();
            endMenu.Clean();
            SDL_Quit();
        }

        public static void CheckQuit(ref bool running, SDL_Event e)
        {
            switch (e.type)
            {
                case SDL_EventType.SDL_QUIT:
                    running = false;
                    break;
            }
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
                Console.WriteLine($"There was an issue initializing SDL. {SDL_GetError()}");

            // Initialize TTF
            if (TTF_Init() < 0)
                Console.WriteLine($"There was an issue initializing TTF. {SDL_GetError()}");
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
    
        public static IntPtr CreateTextureFromPos(IntPtr renderer, SDL_Rect src, SDL_Rect dst, string path)
        {
            var tex = SDL_CreateTexture(renderer, SDL_PIXELFORMAT_UNKNOWN, 2, dst.w, dst.h);
            var p = SDL_GetRenderTarget(renderer);

            var img = CreateTexture(renderer, CreateImgSurface(path));
            SDL_SetRenderTarget(renderer, tex);
            SDL_RenderCopy(renderer, img, ref src, ref dst);
            SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND);

            SDL_SetRenderTarget(renderer, p);
            SDL_DestroyTexture(img);
            return tex;
        }
    
        public static IntPtr CopyTexture(IntPtr renderer, IntPtr texture)
        {
            int w, h;
            SDL_QueryTexture(texture, out _, out _, out w, out h);
            IntPtr tex = SDL_CreateTexture(renderer, SDL_PIXELFORMAT_UNKNOWN, 2, w, h);

            var p = SDL_GetRenderTarget(renderer);
            SDL_SetRenderTarget(renderer, tex);
            SDL_RenderCopy(renderer, texture, IntPtr.Zero, IntPtr.Zero);
            SDL_SetRenderTarget(renderer, p);

            SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND);
            return tex;
        }
    
        public static IntPtr OpenFont(int size)
        {
            var font = TTF_OpenFont("res/font/easter.ttf", size);
            if (font == IntPtr.Zero)
                Console.WriteLine("Font was not found. SDL_Error: " + SDL_GetError());
            
            return font;
        }  
    
    }
}