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
            throw new NotImplementedException();
        }

        public void Leave()
        {
            throw new NotImplementedException();
        }

        public void Monologue()
        {
            throw new NotImplementedException();
        }

        public bool Rand()
        {
            throw new NotImplementedException();
        }

        public void React(IDwarf dwarf)
        {
            throw new NotImplementedException();
        }

        private void Msg(object msg)
        {
            Controller.WriteMsg($"{Name}: {msg}");
        }
    }
}