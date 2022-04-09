namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Brille (Doc)
        private void BuildDoc()
        {
            RandomReaction = new List<Action>() { DocRan1, DocRan2, DocRan3 };
            JoinReaction = new List<Action>() { DocJoin1, DocJoin2, DocJoin3, DocJoin4, DocJoin5 };
            LeaveReaction = new List<Action>() { DocLeave1, DocLeave2, DocLeave3 };
            MonoReaction = new List<Action>() { DocMono1, DocMono2, DocMono3 };
            DoubleReaction = new List<Action<DwarfNames>>() { DocDouble1, DocDouble2, DocDouble3, DocDouble4 };
        }

        #region Random
        void DocRan1()
        {
            Msg("Hey, hvem har taget min fileteringskniv?...");
            Msg("Ved I, hvor svært det er at filetér en dværg uden min ynglingskniv...");
        }
        void DocRan2()
        {
            Msg("Vidste i at dværge i denne historie har en tendens til at elske den samme pige?");
        }
        void DocRan3()
        {
            DwarfNames d = (DwarfNames)Rng.Next(0, 7);
            Msg($"Hey {d} hjælp mig lige med benet her. Det er lidt sejt at tygge igennem.");
            Controller.AddDwarf(d);
        }
        #endregion
        
        #region Join
        void DocJoin1()
        {
            Msg("Jeg vidste I ikke kunne klare jer uden mig.");
        }
        void DocJoin2()
        {
            Msg("Hvad skal jeg nu hjælpe jer med?!");
        }
        void DocJoin3()
        {
            Msg("I kan da heller ikke klare noget selv!");
        }
        void DocJoin4()
        {
            DwarfNames d = (DwarfNames)Rng.Next(0, 7);
            Msg($"Ja ja, jeg kommer nu. {d} jeg skal lige bruge dig");
            Controller.AddDwarf(d);
        }
        void DocJoin5()
        {
            Msg("Drenge jeg er sgu for vigtig til jeres plader!");
            Controller.RemoveDwarf(Name);
        }
        #endregion

        #region Leave
        void DocLeave1()
        {
            Msg("Der er alligevel ingen der værdsætter mit lederskab");
        }
        void DocLeave2()
        {
            Msg("Min tid forbi, den nu er.");
        }
        void DocLeave3()
        {
            Msg("Det her er en svær stund for mig, men en rigtig leder må problemet i øjnene.");
            Msg("...");
            Msg("Og denne gang er problemet mig.");
        }
        #endregion

        #region Mono
        void DocMono1()
        {
            Msg("Det kan ikke være sandt jeg omgås er sådan en flok tåber.");
        }
        void DocMono2()
        {
            Msg("Hvordan kan jeg være sidst? Jeg er lederen, jeg bør være først".Reverse());
        }
        void DocMono3()
        {
            Msg("Det må være tid til at slibe min kniv igen.");
        }
        #endregion

        #region Double
        void DocDouble1(DwarfNames dwarf)
        {
            Msg($"Hey {dwarf}, kan jeg få lov at teste min nye operationskniv på dig?");
        }
        void DocDouble2(DwarfNames dwarf)
        {
            Msg($"Vidste du, mine fine {dwarf}, at det ikke mine briller der gør mig klogere end dig?");
        }
        void DocDouble3(DwarfNames dwarf)
        {
            Msg($"Hør efter! Hør efter! {dwarf} vil du lige dæmåe dig, jeg prøver at tale her.");
        }
        void DocDouble4(DwarfNames dwarf)
        {
            Controller.WriteMsg($"{Name} er blevet ret træt af {dwarf}.");
            Msg($"Nu må det stoppe. {dwarf} du må forlade rummet, hvis det er sådan du skal opføre dig!");
            Controller.RemoveDwarf(dwarf);
        }
        #endregion
    }
}