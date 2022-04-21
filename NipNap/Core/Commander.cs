
namespace NipNap.Core
{
    public class Commander
    {
        private bool running = false;
        private NipNapper Nip;

        public Commander(NipNapper nip)
        {
            Nip = nip;
        }

        public void Start()
        {
            running = true;

            while (running)
            {
                Nip.AskQuestion();
            }
        }
    }
}