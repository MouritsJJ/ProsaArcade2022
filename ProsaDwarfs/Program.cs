using ProsaDwarfs.Core;

namespace ProsaDwarfs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IController c = new DwarfController();
            
            bool running = true;
            while (running)
            {
                Console.WriteLine("This is a story about The Seven Dwarfs\n");

                c.Start();

                Console.WriteLine("\nThe End!\n");

                Console.Write("Would you like to meet The Seven Dwarfs again? [y/n] : ");
                string? ans = Console.ReadLine();
                if (ans.Trim().ToLower().StartsWith('n')) running = false;
            }
        }
    }
}