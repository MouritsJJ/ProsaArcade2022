using static SDL2.SDL;
using static Easter.Core.Utilities;
using Easter.Objects;

namespace Easter.Core
{
    public static class StartFunc
    {
        public static void CreateBG(App app)
        {
            var texture = SDL_CreateTexture(app.Renderer, SDL_PIXELFORMAT_UNKNOWN, 2, app.Width, app.Height);
            var tex = SDL_CreateTexture(app.Renderer, SDL_PIXELFORMAT_UNKNOWN, 0, 40, 40);
            var surface = CreateImgSurface("res/pic/bg_tile.png");
            var imgTex = CreateTexture(app.Renderer, surface);

            SDL_Rect rec = new SDL_Rect()
            {
                h = 40,
                w = 40
            };

            IntPtr p = SDL_GetRenderTarget(app.Renderer);
            SDL_SetRenderTarget(app.Renderer, texture);
            for (int w = 0; w < app.Width; w += 40)
                for (int h = 0; h < app.Height; h += 40)
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
    
        
    }
}