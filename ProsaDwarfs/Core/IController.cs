using ProsaDwarfs.Dwarfs;

namespace ProsaDwarfs.Core
{
    public interface IController
    {
        public DwarfNames GetNewDwarf();
        public void AddDwarf(DwarfNames dwarf);
        public void RemoveDwarf(DwarfNames dwarf);
        public void WriteMsg(object msg);
        public void Start();
    }
}