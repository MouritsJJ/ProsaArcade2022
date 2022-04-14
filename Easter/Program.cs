using static Easter.Core.Utilities;
using static Easter.Core.StartFunc;
using Easter.Objects;

namespace Easter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool running = true;
            App app = new App(1280, 960, 40);
            Setup(app);
            LoadResources(app);

            while (running)
            {
                PollEvents(ref running);
                Render(app);
            }

            CleanUp(app);
        }
        
    }
}