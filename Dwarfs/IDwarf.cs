namespace ProsaDwarfs.Dwarfs 
{
    public enum DwarfNames { Brille, Gnavpot, Lystig, Prosit, Flovmand, Søvnig, Dumpe }
    public interface IDwarf
    {
        public DwarfNames Name { get; }
        public void React(IDwarf dwarf);
        public void React();
    }
}