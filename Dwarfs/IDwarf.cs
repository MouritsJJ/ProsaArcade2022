namespace ProsaDwarfs.Dwarfs 
{
    public enum DwarfNames { Brille, Gnavpot, Lystig, Prosit, Flovmand, Søvnig, Dumpe }
    public interface IDwarf
    {
        public DwarfNames Name { get; }
        public bool Rand();
        public void Join();
        public void Leave();
        public void Monologue();
        public void React(IDwarf dwarf);
    }
}