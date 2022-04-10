using ProsaDwarfs.Dwarfs;

namespace ProsaDwarfs.Core
{
    public class DwarfController : IController
    {
        private int SnowWhiteChance = 5;
        private int Counter = 0;
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
            if (Dwarfs.Count == Enum.GetNames(typeof(DwarfNames)).Count()) return (DwarfNames)d;
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
                Counter = 0;
                Console.ReadKey();
            }
        }

        public void RemoveDwarf(DwarfNames dwarf)
        {
            if (Dwarfs.Select(d => d.Name).Contains(dwarf))
            {
                IDwarf? d = Dwarfs.Find(d => d.Name == dwarf);
                Dwarfs.Remove(d);
                d.Leave();
                PrintDwarfs();
                Counter = 0;
                Console.ReadKey();
            }
        }

        public void Start()
        {
            this.Dwarfs.Add(new Dwarf(this.GetNewDwarf(), this));
            this.Dwarfs.Add(new Dwarf(this.GetNewDwarf(), this));

            this.PrintDwarfs();
            int DCounter = -1;
            while(Counter < 3 && Dwarfs.Count > 0)
            {
                int d = ++DCounter % Dwarfs.Count;
                // Random reactions
                foreach (IDwarf dwarf in Dwarfs) if (dwarf.Rand()) break;
                if (!SnowWhiteLeave()) SnowWhiteJoin();

                // Original reaction
                if (d + 1 < Dwarfs.Count) Dwarfs[d].React(Dwarfs[d + 1]);
                // Last in list
                else if (d + 1 == Dwarfs.Count) Dwarfs[d].Monologue();
                this.WriteMsg("");
                Console.ReadKey();
                Counter++;
            }
            // Nothing changes for more than 3 reaction rounds
            if (Counter >= 3)
            {
                this.WriteMsg("Snehvide spottes ud af vinduet!");
                this.WriteMsg("Alle dværge løber straks efter hende.");
                this.WriteMsg("");
                int c = Dwarfs.Count;
                for (int dwar = 0; dwar < c; ++dwar) this.RemoveDwarf(Dwarfs[0].Name);
                return;
            }
        }

        public void WriteMsg(object msg)
        {
            Console.WriteLine(msg);
        }

        private void PrintDwarfs()
        {
            if (Dwarfs.Count == 0) return;
            string msg = "\n[ ";
            foreach (IDwarf d in Dwarfs.SkipLast(1)) 
                msg += $"{d.Name.ToString()}, ";
            if (Dwarfs.Count > 0) msg += $"{Dwarfs.Last().Name.ToString()}";
            msg += " ]\n";
            this.WriteMsg(msg);
        }

        private bool SnowWhiteLeave()
        {
            int c = rng.Next(0, 100);
            if (c >= SnowWhiteChance) return false;
            DwarfNames d = (DwarfNames)rng.Next(0, Dwarfs.Count);
            this.WriteMsg($"{d} synes han ser Snehvide udenfor og lister derfor stille ud for at prøve at få alenetid med hende");
            this.RemoveDwarf(d);
            return true;
        }

        private bool SnowWhiteJoin()
        {
            int c = rng.Next(0, 100);
            if (c >= SnowWhiteChance) return false;
            this.WriteMsg("Snehvide åbner døren ind til dværgene");
            this.WriteMsg("Alle er straks klar på at være med");
            foreach (DwarfNames d in Enum.GetValues(typeof(DwarfNames))) this.AddDwarf(d);
            return true;
        }
    }
}