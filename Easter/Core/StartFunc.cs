using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL_mixer;
using static Easter.Core.Utilities;
using Easter.Objects;

namespace Easter.Core
{
    public static class StartFunc
    {

        public static void LoadResources(App app, Menu menu)
        {
            LoadEggs(app);
            CreateBG(app);
            LoadBunny(app);
            LoadBump(app);
            LoadAudio(app);
            CreateMenu(app, menu);
        }

        public static void GameMode(ref bool running, App app, Menu menu)
        {
            bool menuRunning = true;
            uint tick, wait;
            while (menuRunning && running)
            {
                tick = SDL_GetTicks();

                PollEvents(ref running, app, menu);
                menu.Update(ref menuRunning, app);
                RenderGameStart(app, menu);

                wait = (uint)(1000 / app.FPS) - (SDL_GetTicks() - tick);
                SDL_Delay((uint)Math.Min(1000 / app.FPS, wait));
            }
        }

        private static void PollEvents(ref bool running, App app, Menu menu)
        {
            // Check to see if there are any events and continue to do so until the queue is empty.
            SDL_PollEvent(out SDL_Event e);
            switch (e.type)
            {
                case SDL_EventType.SDL_QUIT:
                    running = false;
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    menu.MouseClicked = true;
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    menu.MouseClicked = false;
                    break;
            }
        }

        private static void RenderGameStart(App app, Menu menu)
        {
            // Clears the current render surface.
            SDL_RenderClear(app.Renderer);

            // Render bagground tiles
            SDL_RenderCopy(app.Renderer, app.BG, IntPtr.Zero, IntPtr.Zero);

            // Render Bunny
            SDL_RenderCopy(app.Renderer, app.Bunny.Texture, IntPtr.Zero, ref app.Bunny.Pos);

            // Render menu title
            SDL_RenderCopy(app.Renderer, menu.MenuTitle, IntPtr.Zero, ref menu.MenuTitlePos);
            
            // Render buttons
            SDL_RenderCopy(app.Renderer, menu.Seconds30, IntPtr.Zero, ref menu.Seconds30Pos);
            SDL_RenderCopy(app.Renderer, menu.Seconds60, IntPtr.Zero, ref menu.Seconds60Pos);
            SDL_RenderCopy(app.Renderer, menu.Seconds120, IntPtr.Zero, ref menu.Seconds120Pos);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL_RenderPresent(app.Renderer);
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
    
        private static void LoadAudio(App app)
        {
            Mix_OpenAudio(MIX_DEFAULT_FREQUENCY, MIX_DEFAULT_FORMAT, 2, 1024);
            app.BG_Music = Mix_LoadMUS("res/audio/easter.wav");
            if (app.BG_Music == IntPtr.Zero)
                Console.WriteLine("Could not load music. SDL Error: " + SDL_GetError());
            app.Egg_Sound = Mix_LoadWAV("res/audio/egg.wav");
            app.Menu_Sound = Mix_LoadWAV("res/audio/menu.wav");
        }
    
        private static void CreateMenu(App app, Menu menu)
        {
            var font = OpenFont(120);

            // Menu title
            SDL_Color color = new SDL_Color() { r = 13, g = 109, b = 253, a = 255 };
            menu.MenuTitle = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "Seconds", color));
            SDL_QueryTexture(menu.MenuTitle, out _, out _, out int w, out int h);
            menu.MenuTitlePos = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = app.Height - 8*app.TileSize };

            // 30 seconds
            color = new SDL_Color() { r = 255, g = 255, b = 255, a = 255 };
            menu.Seconds30Standard = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "30", color));
            color = new SDL_Color() { r = 13, g = 109, b = 253, a = 255 };
            menu.Seconds30Highlight = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "30", color));
            SDL_QueryTexture(menu.Seconds30Standard, out _, out _, out w, out h);
            menu.Seconds30Pos = new SDL_Rect() { w = w, h = h, x = 5*app.TileSize, y = app.Height - 4*app.TileSize };
            menu.Seconds30 = menu.Seconds30Standard;

            // 60 seconds
            color = new SDL_Color() { r = 255, g = 255, b = 255, a = 255 };
            menu.Seconds60Standard = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "60", color));
            color = new SDL_Color() { r = 13, g = 109, b = 253, a = 255 };
            menu.Seconds60Highlight = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "60", color));
            SDL_QueryTexture(menu.Seconds60Standard, out _, out _, out w, out h);
            menu.Seconds60Pos = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = app.Height - 4*app.TileSize };
            menu.Seconds60 = menu.Seconds60Standard;

            // 120 seconds
            color = new SDL_Color() { r = 255, g = 255, b = 255, a = 255 };
            menu.Seconds120Standard = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "120", color));
            color = new SDL_Color() { r = 13, g = 109, b = 253, a = 255 };
            menu.Seconds120Highlight = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "120", color));
            SDL_QueryTexture(menu.Seconds120Standard, out _, out _, out w, out h);
            menu.Seconds120Pos = new SDL_Rect() { w = w, h = h, x = (app.Width - w) - 3*app.TileSize, y = app.Height - 4*app.TileSize };
            menu.Seconds120 = menu.Seconds120Standard;
        }
    }
}