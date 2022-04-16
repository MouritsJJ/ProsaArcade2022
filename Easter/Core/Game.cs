using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static Easter.Core.Utilities;
using static Easter.Objects.Colors;
using Easter.Objects;

namespace Easter.Core
{
    public static class Game
    {
        public static void GameLoop(ref bool running, App app, Menu menu)
        {
            uint tick, wait;
            while (running && app.Seconds < app.GameTime)
            {
                tick = SDL_GetTicks();
                if (++app.Frames == app.FPS) { app.Seconds++; app.Frames = 0; }

                PollEvents(ref running, app);
                UpdateApp(app);
                Render(app);

                wait = (uint)(1000 / app.FPS) - (SDL_GetTicks() - tick);
                SDL_Delay((uint)Math.Min(1000 / app.FPS, wait));
            }

            // Reset bunny
            app.Bunny.Texture = app.Bunny.South;
            app.Bunny.Pos = new SDL_Rect()
            {
                h = 2*app.TileSize,
                w = 2*app.TileSize,
                x = (app.Width / 2) - app.TileSize,
                y = (app.Height / 2) + app.TileSize
            };
        }

        private static void UpdateApp(App app)
        {
            // Move bunny
            app.Bunny.UpdatePos(app);

            // Generate bumps
            if (app.Bumps.Count < app.MaxBumps && ((int)app.Seconds * app.FPS + app.Frames) - app.LastBump > 10)
            {
                SDL_Rect rec = new SDL_Rect{ w = app.EarthBump.Pos.w, h = app.EarthBump.Pos.h, 
                    x = app.Rng.Next(0, app.Width - app.EarthBump.Pos.w),
                    y = app.Rng.Next(app.TileSize, app.Height - app.EarthBump.Pos.h) };
                int egg = DetermineEgg(app);
                app.Bumps.Add(new Bump() 
                {
                    Pos = rec,
                    Texture = CopyTexture(app.Renderer, app.EarthBump.Texture),
                    Egg = CopyTexture(app.Renderer, app.Eggs[egg]),
                    Points = app.EggPoints[egg]
                });
                app.LastBump = (int)((int)app.Seconds * app.FPS + app.Frames);
            }

            // Update bumps
            for (int b = 0; b < app.Bumps.Count; ++b)
                if (app.Bumps[b].Update(app)) app.Bumps.RemoveAt(b--);
        }

        private static void Render(App app)
        {
            // Clears the current render surface.
            SDL_RenderClear(app.Renderer);

            // Render bagground tiles
            SDL_RenderCopy(app.Renderer, app.BG, IntPtr.Zero, IntPtr.Zero);

            // Count down seconds
            int sec = app.GameTime - app.Seconds;
            var font = OpenFont(app.TileSize);
            var tex = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, sec.ToString(), White));
            SDL_QueryTexture(tex, out _, out _, out int w, out int h);
            var pos = new SDL_Rect() { w = w, h = h, y = 0, x = app.Width - 8*app.TileSize - w };
            SDL_RenderCopy(app.Renderer, tex, IntPtr.Zero, ref pos);

            // Render score
            font = OpenFont(app.TileSize);
            tex = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, app.Points.ToString(), Yellow));
            SDL_QueryTexture(tex, out _, out _, out w, out h);
            SDL_Rect rec = new SDL_Rect() { w = w, h = h, y = 0, x = app.Width - (app.TileSize + w + 5) };
            SDL_RenderCopy(app.Renderer, tex, IntPtr.Zero, ref rec);
            SDL_DestroyTexture(tex);
            TTF_CloseFont(font);

            // Render Bumps
            foreach (Bump b in app.Bumps)
                SDL_RenderCopy(app.Renderer, b.Texture, IntPtr.Zero, ref b.Pos);

            // Render Bunny
            SDL_RenderCopy(app.Renderer, app.Bunny.Texture, IntPtr.Zero, ref app.Bunny.Pos);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL_RenderPresent(app.Renderer);
        }

        private static void PollEvents(ref bool running, App app)
        {
            // Check to see if there are any events
            SDL_PollEvent(out SDL_Event e);
            CheckQuit(ref running, e);
            bool keydown = false;
            bool keyup = false;
            switch (e.type)
            {
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
        }
    
        public static void BlockingBorders(App app, ref SDL_Rect rec)
        {
            if (rec.x < 0) rec.x = 0;
            if (rec.x + rec.w > app.Width) rec.x = app.Width - rec.w;
            if (rec.y < 0) rec.y = 0;
            if (rec.y + rec.h > app.Height) rec.y = app.Height - rec.h;
        }
    
        public static bool CollisionDetection(SDL_Rect X, SDL_Rect Y)
        {
            // Top left
            if (X.x >= Y.x && X.x <= Y.x + Y.w && X.y >= Y.y && X.y <= Y.y + Y.h) return true;
            // Top right
            if (X.x + X.w >= Y.x && X.x + X.w <= Y.x + Y.w && X.y >= Y.y && X.y <= Y.y + Y.h) return true;
            // Bottom left
            if (X.x >= Y.x && X.x <= Y.x + Y.w && X.y + X.h >= Y.y && X.y + X.h <= Y.y + Y.h) return true;
            // Bottom right
            if (X.x + X.w >= Y.x && X.x + X.w <= Y.x + Y.w && X.y + X.h >= Y.y && X.y + X.h <= Y.y + Y.h) return true;
            
            // No collision
            return false;
        }
    
        private static int DetermineEgg(App app)
        {
            int c = app.Rng.Next(0, 1000);
            if (c < 5)      return 5;
            if (c < 20)     return 4;
            if (c < 100)    return 3;
            if (c < 250)    return 2;
            if (c < 500)    return 1;
            return 0;
        }

    }
}