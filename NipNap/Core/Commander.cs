
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
            Nip.Reset();

            while (running)
            {
                running = !Nip.AskQuestion();
            }

            Nip.GetResult();
        }
    }
}