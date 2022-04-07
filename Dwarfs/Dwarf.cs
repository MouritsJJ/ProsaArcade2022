using ProsaDwarfs.Core;

namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf : IDwarf
    {
        public DwarfNames Name { get; }

        private List<Func<bool>> RandomReaction = new List<Func<bool>>();

        private List<Action> JoinReaction = new List<Action>();

        private List<Action> LeaveReaction = new List<Action>();

        private List<Action> MonoReaction = new List<Action>();

        private List<Action> DoubleReaction = new List<Action>();

        private IController Controller;

        public Dwarf(DwarfNames name, IController c)
        {
            Name = name;
            Controller = c;
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
    }
}