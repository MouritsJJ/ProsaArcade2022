using static SDL2.SDL;
using static Easter.Core.Utilities;
using Easter.Objects;

namespace Easter.Core
{
    public static class StartFunc
    {

        public static void LoadResources(App app)
        {
            CreateBG(app);
            LoadEggs(app);
            LoadBunny(app);
        }
        private static void CreateBG(App app)
        {
            var texture = SDL_CreateTexture(app.Renderer, SDL_PIXELFORMAT_UNKNOWN, 2, app.Width, app.Height);
            var tex = SDL_CreateTexture(app.Renderer, SDL_PIXELFORMAT_UNKNOWN, 0, 2*app.TileSize, 2*app.TileSize);
            var surface = CreateImgSurface("res/pic/bg_tile.png");
            var imgTex = CreateTexture(app.Renderer, surface);

            SDL_Rect rec = new SDL_Rect()
            {
                h = 2*app.TileSize,
                w = 2*app.TileSize
            };

            IntPtr p = SDL_GetRenderTarget(app.Renderer);
            SDL_SetRenderTarget(app.Renderer, texture);
            for (int w = 0; w < app.Width; w += 2*app.TileSize)
                for (int h = 0; h < app.Height; h += 2*app.TileSize)
                {
                    rec.x = w;
                    rec.y = h;
                    SDL_RenderCopy(app.Renderer, imgTex, IntPtr.Zero, ref rec);
                }
            SDL_SetRenderTarget(app.Renderer, p);

            SDL_DestroyTexture(tex);
            SDL_DestroyTexture(imgTex);

            app.BG = texture;
        }
    
        private static void LoadEggs(App app)
        {
            List<IntPtr> eggs = new List<IntPtr>();
            var img = CreateTexture(app.Renderer, CreateImgSurface("res/pic/eggs.png"));

            var p = SDL_GetRenderTarget(app.Renderer);
            for (int e = 0; e < 6; ++e)
            {
                var tex = SDL_CreateTexture(app.Renderer, SDL_PIXELFORMAT_UNKNOWN, 2, app.TileSize, app.TileSize);
                SDL_SetRenderTarget(app.Renderer, tex);
                SDL_Rect src = new SDL_Rect() { w = 23, h = 27, x = 23*e };
                SDL_RenderCopy(app.Renderer, img, ref src, IntPtr.Zero);
                SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND);
                eggs.Add(tex);
            }
            SDL_SetRenderTarget(app.Renderer, p);

            app.Eggs = eggs;
        }
    
        private static void LoadBunny(App app)
        {
            string path = "res/pic/bunny_";

            app.Bunny.East = CreateTextureFromPos(app.Renderer,
                new SDL_Rect() { w = 50, h = 50 },
                new SDL_Rect() { w = app.TileSize, h = app.TileSize },
                path + "east.png");
            app.Bunny.West = CreateTextureFromPos(app.Renderer,
                new SDL_Rect() { w = 50, h = 50 },
                new SDL_Rect() { w = app.TileSize, h = app.TileSize },
                path + "west.png");
            app.Bunny.North = CreateTextureFromPos(app.Renderer,
                new SDL_Rect() { w = 50, h = 50 },
                new SDL_Rect() { w = app.TileSize, h = app.TileSize },
                path + "north.png");
            app.Bunny.South = CreateTextureFromPos(app.Renderer,
                new SDL_Rect() { w = 50, h = 50 },
                new SDL_Rect() { w = app.TileSize, h = app.TileSize },
                path + "south.png");
            app.Bunny.Texture = app.Bunny.South;
            
            app.Bunny.Pos = new SDL_Rect()
            {
                h = 2*app.TileSize,
                w = 2*app.TileSize,
                x = (app.Width / 2) - app.TileSize,
                y = (app.Height / 2) + app.TileSize
            };
        }

    }
}