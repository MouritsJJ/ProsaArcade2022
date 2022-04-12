using SDL2;
using static Easter.Core.SDLHelper;
using Easter.Core;

namespace Easter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool running = true;
            App app = new App();
            Setup(app);

            while (running)
            {
                PollEvents(ref running);
                Render(app);
            }

            CleanUp(app);
        }
        
    }
}