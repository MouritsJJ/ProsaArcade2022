using ProsaDwarfs.Dwarfs;

namespace ProsaDwarfs.Core
{
    public class DwarfController : IController
    {
        private List<IDwarf> Dwarfs = new List<IDwarf>();
        private Random rng = new Random();

        public DwarfController()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }

        public DwarfNames GetNewDwarf()
        {
            int d = rng.Next(0, 7);
            while (Dwarfs.Select(d => d.Name).Contains((DwarfNames)d))
                d = rng.Next(0, 7);
            return (DwarfNames)d;
        }

        public void AddDwarf(DwarfNames dwarf)
        {
            if (!Dwarfs.Select(d => d.Name).Contains(dwarf))
            {
                IDwarf d = new Dwarf(dwarf, this);
                Dwarfs.Add(d);
                d.Join();
                PrintDwarfs();
            }
        }

        public void RemoveDwarf(DwarfNames dwarf)
        {
            if (Dwarfs.Select(d => d.Name).Contains(dwarf))
            {
                IDwarf d = Dwarfs.Find(d => d.Name == dwarf);
                Dwarfs.Remove(d);
                d.Leave();
                PrintDwarfs();
            }
        }

        public void Start()
        {
            this.Dwarfs.Add(new Dwarf(this.GetNewDwarf(), this));
            this.Dwarfs.Add(new Dwarf(this.GetNewDwarf(), this));

            this.PrintDwarfs();
            while (Dwarfs.Count != 0)
            {
                // Chain reaction
                int d;
                for (d = 0; d < Dwarfs.Count - 1; ++d)
                {
                    // Random reactions
                    foreach (IDwarf dwarf in Dwarfs) if (dwarf.Rand()) break;

                    // Original reaction
                    Dwarfs[d].React(Dwarfs[d + 1]);
                }
                // Last in list reaction
                Dwarfs[d].Monologue();
            }
        }

        public void WriteMsg(object msg)
        {
            Console.WriteLine(msg);
        }

        private void PrintDwarfs()
        {
            string msg = "\n[ ";
            foreach (IDwarf d in Dwarfs.SkipLast(1)) 
                msg += $"{d.Name.ToString()}, ";
            msg += $"{Dwarfs.Last().Name.ToString()} ]\n";
            this.WriteMsg(msg);
        } 
    }
}