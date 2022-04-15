using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static Easter.Core.Utilities;
using Easter.Objects;

namespace Easter.Core
{
    public static class StartFunc
    {

        public static void LoadResources(App app)
        {
            LoadEggs(app);
            CreateBG(app);
            LoadBunny(app);
            LoadBump(app);
        }
       
        private static void CreateBG(App app)
        {
            var texture = SDL_CreateTexture(app.Renderer, SDL_PIXELFORMAT_UNKNOWN, 2, app.Width, app.Height);
            var imgTex = CreateTexture(app.Renderer, CreateImgSurface("res/pic/bg_tile.png"));

            SDL_Rect rec = new SDL_Rect()
            {
                h = 2*app.TileSize,
                w = 2*app.TileSize
            };

            IntPtr p = SDL_GetRenderTarget(app.Renderer);
            SDL_SetRenderTarget(app.Renderer, texture);

            // Repeat tiles
            int w, h;
            for (w = 0; w < app.Width; w += 2*app.TileSize)
                for (h = app.TileSize; h < app.Height; h += 2*app.TileSize)
                {
                    rec.x = w;
                    rec.y = h;
                    SDL_RenderCopy(app.Renderer, imgTex, IntPtr.Zero, ref rec);
                }
            // Add score eggs
            IntPtr fontSurface, fontTex;
            var font = OpenFont(app.TileSize);
            SDL_Color color = new SDL_Color() { r = 255, g = 255, b = 255, a = 255 };
            for (int e = 0; e < app.EggPoints.Count(); ++e)
            {
                rec = new SDL_Rect() { h = app.TileSize, w = app.TileSize, y = 0,
                x = e < 3 ? 3*app.TileSize*e : 3*app.TileSize*3 + 4*app.TileSize*(e - 3) };
                SDL_RenderCopy(app.Renderer, app.Eggs[e], IntPtr.Zero, ref rec);

                fontSurface = TTF_RenderText_Blended(font, app.EggPoints[e].ToString(), color);
                fontTex = CreateTexture(app.Renderer, fontSurface);
                SDL_QueryTexture(fontTex, out _, out _, out w, out h);
                rec = new SDL_Rect() { w = w, h = h, x = rec.x + app.TileSize + 5, y = 0 };
                SDL_RenderCopy(app.Renderer, fontTex, IntPtr.Zero, ref rec);
            }

            // Add Scoring P
            color = new SDL_Color() { r = 0, g = 208, b = 255, a = 255 };
            fontSurface = TTF_RenderText_Blended(font, "p", color);
            fontTex = CreateTexture(app.Renderer, fontSurface);
            SDL_QueryTexture(fontTex, out _, out _, out w, out h);
            rec = new SDL_Rect() { h = h, w = w, y = 0, x = app.Width - w };
            SDL_RenderCopy(app.Renderer, fontTex, IntPtr.Zero, ref rec);


            SDL_SetRenderTarget(app.Renderer, p);

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

        private static void LoadBump(App app)
        {
            string path = "res/pic/earth_bump.png";
            app.EarthBump.Texture = CreateTextureFromPos(app.Renderer,
                new SDL_Rect() { w = 50, h = 14, y = 50 - 14 },
                new SDL_Rect() { w = 40, h = 11 },
                path);
            app.EarthBump.Pos = new SDL_Rect() { w = 40, h = 11 };
        }
    }
}