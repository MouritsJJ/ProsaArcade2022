using NipNap.Core;

namespace NipNap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NipNapper n = new NipNapper();
            Commander c = new Commander(n);
            for (int i = 0; i < 3; ++i) c.Start();
        }
    }
}