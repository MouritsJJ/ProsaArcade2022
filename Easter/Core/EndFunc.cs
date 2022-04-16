using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static Easter.Core.Utilities;
using static Easter.Objects.Colors;
using Easter.Objects;

namespace Easter.Core
{
    public static class EndFunc
    {
        public static void EndScreen(ref bool running, App app, EndMenu endMenu)
        {
            bool endRunning = true;
            uint tick, wait;
            while (endRunning && running)
            {
                tick = SDL_GetTicks();

                PollEvents(ref running, endMenu);
                endMenu.Update(ref endRunning, app);
                RenderEndScreen(app, endMenu);

                wait = (uint)(1000 / app.FPS) - (SDL_GetTicks() - tick);
                SDL_Delay((uint)Math.Min(1000 / app.FPS, wait));
            }

            endMenu.Reset();
            app.Reset();
            app.Bunny.Reset();
        }

        private static void PollEvents(ref bool running, EndMenu menu)
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

        private static void RenderEndScreen(App app, EndMenu menu)
        {
            if (!menu.Loaded)
            {
                menu.Loaded = true;
                LoadAssets(app, menu);
            }

            // Clears the current render surface.
            SDL_RenderClear(app.Renderer);

            // Render bagground tiles
            SDL_RenderCopy(app.Renderer, app.BG, IntPtr.Zero, IntPtr.Zero);

            // Render Bunny
            SDL_RenderCopy(app.Renderer, app.Bunny.Texture, IntPtr.Zero, ref app.Bunny.Pos);

            // Render menu title
            SDL_RenderCopy(app.Renderer, menu.ScoreTitle, IntPtr.Zero, ref menu.ScoreTitlePos);

            // Render score
            SDL_RenderCopy(app.Renderer, menu.Score, IntPtr.Zero, ref menu.ScorePos);
            SDL_RenderCopy(app.Renderer, menu.ScoreP, IntPtr.Zero, ref menu.ScorePPos);

            // Render Eggs
            SDL_Rect rec = new SDL_Rect();
            for (int e = 0; e < menu.EggsPos.Count; ++e)
            {
                rec = menu.EggsPos[e];
                SDL_RenderCopy(app.Renderer, app.Eggs[e], IntPtr.Zero, ref rec);
                rec = menu.EggsScorePos[e];
                SDL_RenderCopy(app.Renderer, menu.EggsScore[e], IntPtr.Zero, ref rec);
            }

            // Render new game
            SDL_RenderCopy(app.Renderer, menu.NewGame, IntPtr.Zero, ref menu.NewGamePos);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL_RenderPresent(app.Renderer);
        }

        private static void LoadAssets(App app, EndMenu menu)
        {
            // Open font
            var font = OpenFont(120);
            var smallFont = OpenFont(app.TileSize);

            // Score
            menu.Score = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, app.Points.ToString(), White));
            SDL_QueryTexture(menu.Score, out _, out _, out int w, out int h);
            menu.ScorePos = new SDL_Rect() { w = w, h = h, x = (app.Width - w) / 2, y = h + 3*app.TileSize };

            // Score P
            menu.ScoreP = CreateTexture(app.Renderer, TTF_RenderText_Blended(font, "p", Lightblue));
            SDL_QueryTexture(menu.ScoreP, out _, out _, out w, out h);
            menu.ScorePPos = new SDL_Rect() { w = w, h = h, y = h + 3*app.TileSize, 
                x = menu.ScorePos.x + menu.ScorePos.w + app.TileSize };

            // Change font size
            TTF_CloseFont(font);
            font = OpenFont(app.TileSize);

            // Eggs count
            for (int e = 0; e < app.EggCounts.Count; ++e)
            {
                menu.EggsScore.Add(CreateTexture(app.Renderer, TTF_RenderText_Blended(smallFont, app.EggCounts[e].ToString(), White)));
                SDL_QueryTexture(menu.EggsScore[e], out _, out _, out w, out h);
                menu.EggsScorePos[e] = new SDL_Rect() { w = w, h = h,
                    x = menu.EggsScorePos[e].x, y = menu.EggsScorePos[e].y };
            }

            // Close font
            TTF_CloseFont(font);
        }
    }
}