using static SDL2.SDL;

namespace Easter.Objects
{
    public class App
    {
        public App(int width, int height, int tileSize, int velocity, int fPS, int maxBumps)
        {
            Width = width;
            Height = height;
            TileSize = tileSize;
            Velocity = velocity;
            FPS = fPS;
            MaxBumps = maxBumps;
        }

        // Resources for the game
        public IntPtr Window { get; set; }
        public IntPtr Renderer { get; set; }
        public IntPtr BG { get; set; }
        public IntPtr BG_Music { get; set; }
        public IntPtr Egg_Sound { get; set; }
        public IntPtr Menu_Sound { get; set; }
        public List<Bump> Bumps { get; set; } = new List<Bump>();
        public Bump EarthBump { get; set; } = new Bump();
        public int LastBump { get; set; } = 0;
        public List<IntPtr> Eggs { get; set; } = new List<IntPtr>();
        public readonly int[] EggPoints = { 1, 2, 5, 10, 30, 50 };
        public Random Rng { get; set; } = new Random();
        public Bunny Bunny { get; set; } = new Bunny();

        // Information about the App
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileSize { get; set; }
        public int Velocity { get; set; }
        public int FPS { get; set; }
        public int MaxBumps { get; set; }
        public ulong Seconds { get; set; }
        public uint Frames { get; set; }
        public int Points { get; set; } = 0;
    }
}