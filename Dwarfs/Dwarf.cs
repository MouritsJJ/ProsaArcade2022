using ProsaDwarfs.Core;

namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf : IDwarf
    {
        public DwarfNames Name { get; }
        private IController Controller;
        private Random Rng = new Random();

        private List<Action> RandomReaction = new List<Action>();
        private List<Action> JoinReaction = new List<Action>();
        private List<Action> LeaveReaction = new List<Action>();
        private List<Action> MonoReaction = new List<Action>();
        private List<Action<DwarfNames>> DoubleReaction = new List<Action<DwarfNames>>();

        public Dwarf(DwarfNames name, IController c)
        {
            Name = name;
            Controller = c;
            
            if (Name == DwarfNames.Brille) BuildDoc();
            if (Name == DwarfNames.Gnavpot) BuildGrumpy();
            if (Name == DwarfNames.Lystig) BuildHappy();
            if (Name == DwarfNames.Prosit) BuildSneezy();
            if (Name == DwarfNames.Flovmand) BuildBashful();
            if (Name == DwarfNames.Søvnig) BuildSleepy();
            if (Name == DwarfNames.Dumpe) BuildDopey();
        }

        public void Join()
        {
            int r = Rng.Next(0, JoinReaction.Count);
            JoinReaction[r]();
        }

        public void Leave()
        {
            int r = Rng.Next(0, LeaveReaction.Count);
            LeaveReaction[r]();
        }

        public void Monologue()
        {
            int r = Rng.Next(0, MonoReaction.Count);
            MonoReaction[r]();
        }

        public bool Rand()
        {
            int c = Rng.Next(0, 100);
            if (c >= 10) return false;
            int r = Rng.Next(0, RandomReaction.Count);
            RandomReaction[r]();
            return true;
        }

        public void React(IDwarf dwarf)
        {
            int r = Rng.Next(0, DoubleReaction.Count);
            DoubleReaction[r](dwarf.Name);
        }

        private void Msg(object msg)
        {
            Controller.WriteMsg($"{Name}: {msg}");
        }
    }
}