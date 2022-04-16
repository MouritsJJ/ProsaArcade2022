using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL_mixer;
using static Easter.Core.Utilities;
using Easter.Objects;

namespace Easter.Core
{
    public static class StartFunc
    {

        public static void LoadResources(App app, Menu menu, EndMenu endMenu)
        {
            LoadEggs(app);
            CreateBG(app);
            LoadBunny(app);
            LoadBump(app);
            LoadAudio(app);
            CreateStartMenu(app, menu);
            CreateEndScreen(app, endMenu);
        }

        public static void GameMode(ref bool running, App app, Menu menu)
        {
            bool menuRunning = true;
            uint tick, wait;
            while (menuRunning && running)
            {
                tick = SDL_GetTicks();

                PollEvents(ref running, menu);
                menu.Update(ref menuRunning, app);
                RenderGameStart(app, menu);

                wait = (uint)(1000 / app.FPS) - (SDL_GetTicks() - tick);
                SDL_Delay((uint)Math.Min(1000 / app.FPS, wait));
            }

            CountDown(ref running, app);
        }

        private static void PollEvents(ref bool running, Menu menu)
        {
            // Check to see if there are any events
            SDL_PollEvent(out SDL_Event e);
            CheckQuit(ref running, e);
            switch (e.type)
            {
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    menu.MouseClicked = true;
                    break;
                default:
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
            SDL_RenderCopy(app.Renderer, menu.MenuTitle1, IntPtr.Zero, ref menu.MenuTitlePos1);
            SDL_RenderCopy(app.Renderer, menu.SecondsTitle, IntPtr.Zero, ref menu.SecondsTitlePos);
            
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
            IntPtr fontTex;
            var font = OpenFont(app.TileSize);
            SDL_Color color = new SDL_Color() { r = 255, g = 255, b = 255, a = 255 };
            for (int e = 0; e < app.EggPoints.Count(); ++e)
            {
                rec = new SDL_Rect() { h = app.TileSize, w = app.TileSize, y = 0,
                x = e < 3 ? 3*app.TileSize*e : 3*app.TileSize*3 + 4*app.TileSize*(e - 3) };
                SDL_RenderCopy(app.Renderer, app.Eggs[e], IntPtr.Zero, ref rec);

                fontTex = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, app.EggPoints[e].ToString(), color));
                SDL_QueryTexture(fontTex, out _, out _, out w, out h);
                rec = new SDL_Rect() { w = w, h = h, x = rec.x + app.TileSize + 5, y = 0 };
                SDL_RenderCopy(app.Renderer, fontTex, IntPtr.Zero, ref rec);
            }

            // Add Scoring P
            color = new SDL_Color() { r = 0, g = 208, b = 255, a = 255 };
            fontTex = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "p", color));
            SDL_QueryTexture(fontTex, out _, out _, out w, out h);
            rec = new SDL_Rect() { h = h, w = w, y = 0, x = app.Width - w };
            SDL_RenderCopy(app.Renderer, fontTex, IntPtr.Zero, ref rec);

            // Add seconds counter
            fontTex = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "s", color));
            SDL_QueryTexture(fontTex, out _, out _, out w, out h);
            rec = new SDL_Rect() { h = h, w = w, y = 0, x = app.Width - w - 7*app.TileSize };
            SDL_RenderCopy(app.Renderer, fontTex, IntPtr.Zero, ref rec);


            SDL_SetRenderTarget(app.Renderer, p);

            SDL_DestroyTexture(imgTex);
            SDL_DestroyTexture(fontTex);
            TTF_CloseFont(font);

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
    
        private static void CreateStartMenu(App app, Menu menu)
        {
            // Open font
            var font = OpenFont(120);

            // Menu title
            SDL_Color color = new SDL_Color() { r = 214, g = 51, b = 132, a = 255 };
            menu.MenuTitle = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "Easter", color));
            SDL_QueryTexture(menu.MenuTitle, out _, out _, out int w, out int h);
            menu.MenuTitlePos = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = 2*app.TileSize };
            menu.MenuTitle1 = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "Time Attack", color));
            SDL_QueryTexture(menu.MenuTitle1, out _, out _, out w, out h);
            menu.MenuTitlePos1 = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = h + 3*app.TileSize };

            // Seconds title
            color = new SDL_Color() { r = 13, g = 109, b = 253, a = 255 };
            menu.SecondsTitle = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "Seconds", color));
            SDL_QueryTexture(menu.SecondsTitle, out _, out _, out w, out h);
            menu.SecondsTitlePos = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = app.Height - 8*app.TileSize };

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

            // Close font
            TTF_CloseFont(font);
        }
    
        private static void CreateEndScreen(App app, EndMenu menu)
        {
            // Open font
            var font = OpenFont(120);

            // Score title
            SDL_Color color = new SDL_Color() { r = 214, g = 51, b = 132, a = 255 };
            menu.ScoreTitle = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "Final Score", color));
            SDL_QueryTexture(menu.ScoreTitle, out _, out _, out int w, out int h);
            menu.ScoreTitlePos = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = 2*app.TileSize };

            // Eggs position
            menu.EggsPos.Add(new SDL_Rect() { w = app.TileSize, h = app.TileSize,
                y = app.Height - 8*app.TileSize, x = app.Width / 4 - app.TileSize });
            menu.EggsPos.Add(new SDL_Rect() { w = app.TileSize, h = app.TileSize,
                y = app.Height - 8*app.TileSize, x = app.Width / 2 - app.TileSize });
            menu.EggsPos.Add(new SDL_Rect() { w = app.TileSize, h = app.TileSize,
                y = app.Height - 8*app.TileSize, x = app.Width - app.Width / 4 - app.TileSize });
            menu.EggsPos.Add(new SDL_Rect() { w = app.TileSize, h = app.TileSize,
                y = app.Height - 6*app.TileSize, x = app.Width / 4 - app.TileSize });
            menu.EggsPos.Add(new SDL_Rect() { w = app.TileSize, h = app.TileSize,
                y = app.Height - 6*app.TileSize, x = app.Width / 2 - app.TileSize });
            menu.EggsPos.Add(new SDL_Rect() { w = app.TileSize, h = app.TileSize,
                y = app.Height - 6*app.TileSize, x = app.Width - app.Width / 4 - app.TileSize });
            
            // Eggs count position
            menu.EggsScorePos.Add(new SDL_Rect() { y = app.Height - 8*app.TileSize, x = app.Width / 4 + app.TileSize / 2 });
            menu.EggsScorePos.Add(new SDL_Rect() { y = app.Height - 8*app.TileSize, x = app.Width / 2 + app.TileSize / 2 });
            menu.EggsScorePos.Add(new SDL_Rect() { y = app.Height - 8*app.TileSize, x = app.Width - app.Width / 4 + app.TileSize / 2 });
            menu.EggsScorePos.Add(new SDL_Rect() { y = app.Height - 6*app.TileSize, x = app.Width / 4 + app.TileSize / 2 });
            menu.EggsScorePos.Add(new SDL_Rect() { y = app.Height - 6*app.TileSize, x = app.Width / 2 + app.TileSize / 2 });
            menu.EggsScorePos.Add(new SDL_Rect() { y = app.Height - 6*app.TileSize, x = app.Width - app.Width / 4 + app.TileSize / 2 });

            // New game
            color = new SDL_Color() { r = 255, g = 255, b = 255, a = 255 };
            menu.NewGameStandard = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "New Game", color));
            color = new SDL_Color() { r = 13, g = 109, b = 253, a = 255 };
            menu.NewGameHighlight = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "New Game", color));
            SDL_QueryTexture(menu.NewGameStandard, out _, out _, out w, out h);
            menu.NewGamePos = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = app.Height - 4*app.TileSize };
            menu.NewGame = menu.NewGameStandard;

            // Close font
            TTF_CloseFont(font);
        }
    
        private static void CountDown(ref bool running, App app)
        {
            int seconds = 4, frame = 0;

            uint tick, wait;
            while (running && seconds > 0)
            {
                tick = SDL_GetTicks();
                if (++frame == app.FPS) { seconds--; frame = 0; }

                // Check to see if there are any events
                SDL_PollEvent(out SDL_Event e);
                CheckQuit(ref running, e);

                // Render
                SDL_RenderClear(app.Renderer);
                SDL_RenderCopy(app.Renderer, app.BG, IntPtr.Zero, IntPtr.Zero);
                SDL_RenderCopy(app.Renderer, app.Bunny.Texture, IntPtr.Zero, ref app.Bunny.Pos);

                var font = OpenFont(120);
                var color = new SDL_Color() { r = 13, g = 109, b = 253, a = 255 };
                var tex = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, seconds == 1 ? "GO" : (seconds - 1).ToString(), color));
                SDL_QueryTexture(tex, out _, out _, out int w, out int h);
                var rec = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = 8*app.TileSize };
                SDL_RenderCopy(app.Renderer, tex, IntPtr.Zero, ref rec);
                SDL_DestroyTexture(tex);
                TTF_CloseFont(font);

                SDL_RenderPresent(app.Renderer);

                wait = (uint)(1000 / app.FPS) - (SDL_GetTicks() - tick);
                SDL_Delay((uint)Math.Min(1000 / app.FPS, wait));
            }
        }
    }
}