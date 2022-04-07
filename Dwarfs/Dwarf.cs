using ProsaDwarfs.Core;

namespace ProsaDwarfs.Dwarfs
{
    public class Dwarf : IDwarf
    {
        public DwarfNames Name { get; }
        private IController Controller;

        public Dwarf(DwarfNames name, IController c)
        {
            Name = name;
            Controller = c;
        }

        public void React(IDwarf dwarf)
        {
            throw new NotImplementedException();
        }

        public void React()
        {
            throw new NotImplementedException();
        }
    }
}