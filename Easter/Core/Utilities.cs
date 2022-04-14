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
    
        public static void PollEvents(ref bool running, App app)
        {
            // Check to see if there are any events and continue to do so until the queue is empty.
            SDL_PollEvent(out SDL_Event e);
            bool keydown = false;
            bool keyup = false;
            switch (e.type)
            {
                case SDL_EventType.SDL_QUIT:
                    running = false;
                    break;
                case SDL_EventType.SDL_KEYDOWN:
                    keydown = true;
                    break;
                case SDL_EventType.SDL_KEYUP:
                    keyup = true;
                    break;
                default:
                    app.Bunny.VelX = 0;
                    app.Bunny.VelY = 0;
                    break;
            }
            if (keydown) switch (e.key.keysym.sym)
            {
                case SDL_Keycode.SDLK_w: case SDL_Keycode.SDLK_UP:
                    app.Bunny.Up = true;
                    break;
                case SDL_Keycode.SDLK_s: case SDL_Keycode.SDLK_DOWN:
                    app.Bunny.Down = true;
                    break;
                case SDL_Keycode.SDLK_a: case SDL_Keycode.SDLK_LEFT:
                    app.Bunny.Left = true;
                    break;
                case SDL_Keycode.SDLK_d: case SDL_Keycode.SDLK_RIGHT:
                    app.Bunny.Right = true;
                    break;
            }
            if (keyup) switch (e.key.keysym.sym)
            {
                case SDL_Keycode.SDLK_w: case SDL_Keycode.SDLK_UP:
                    app.Bunny.Up = false;
                    break;
                case SDL_Keycode.SDLK_s: case SDL_Keycode.SDLK_DOWN:
                    app.Bunny.Down = false;
                    break;
                case SDL_Keycode.SDLK_a: case SDL_Keycode.SDLK_LEFT:
                    app.Bunny.Left = false;
                    break;
                case SDL_Keycode.SDLK_d: case SDL_Keycode.SDLK_RIGHT:
                    app.Bunny.Right = false;
                    break;
            }
            // SDL_FlushEvents(SDL_EventType.SDL_FIRSTEVENT, SDL_EventType.SDL_LASTEVENT);
        }
    
        public static void UpdateApp(App app)
        {
            app.Bunny.UpdatePos(app);
        }

        public static void Render(App app)
        {
            // Clears the current render surface.
            SDL_RenderClear(app.Renderer);

            // Render bagground tiles
            SDL_RenderCopy(app.Renderer, app.BG, IntPtr.Zero, IntPtr.Zero);

            // Render eggs at different coords
            int e = 0;
            var rec = new SDL_Rect();
            foreach (var egg in app.Eggs)
            {
                rec = new SDL_Rect(){ h = app.TileSize, w = app.TileSize, x = app.TileSize*e++ };
                SDL_RenderCopy(app.Renderer, egg, IntPtr.Zero, ref rec);
            }

            // Render Bunny
            SDL_RenderCopy(app.Renderer, app.Bunny.Texture, IntPtr.Zero, ref app.Bunny.Pos);

            // Render Bump
            rec = new SDL_Rect() { h = 11, w = app.TileSize, x = app.TileSize*e++ };
            SDL_RenderCopy(app.Renderer, app.EarthBump, IntPtr.Zero, ref rec);

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
    
        public static IntPtr CreateTextureFromPos(IntPtr renderer, SDL_Rect src, SDL_Rect dst, string path)
        {
            var tex = SDL_CreateTexture(renderer, SDL_PIXELFORMAT_UNKNOWN, 2, dst.w, dst.h);
            var p = SDL_GetRenderTarget(renderer);

            var img = CreateTexture(renderer, CreateImgSurface(path));
            SDL_SetRenderTarget(renderer, tex);
            SDL_RenderCopy(renderer, img, ref src, ref dst);
            SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND);
            SDL_SetRenderTarget(renderer, p);
            return tex;
        }
    
        public static void BlockingBorders(App app, ref SDL_Rect rec)
        {
            if (rec.x < 0) rec.x = 0;
            if (rec.x + rec.w > app.Width) rec.x = app.Width - rec.w;
            if (rec.y < 0) rec.y = 0;
            if (rec.y + rec.h > app.Height) rec.y = app.Height - rec.h;
        }
    }
}