namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Flovmand (Bashful)
        private void BuildBashful()
        {
            RandomReaction = new List<Action>() { BashfulRan1, BashfulRan2, BashfulRan3 };
            JoinReaction = new List<Action>() { BashfulJoin1, BashfulJoin2, BashfulJoin3 };
            LeaveReaction = new List<Action>() { BashfulLeave1, BashfulLeave2, BashfulLeave3 };
            MonoReaction = new List<Action>() { BashfulMono1, BashfulMono2, BashfulMono3 };
            DoubleReaction = new List<Action<DwarfNames>>() { BashfulDouble1, BashfulDouble2, BashfulDouble3 };
        }

        #region Random
        void BashfulRan1()
        {
            Msg("Prut!");
            Controller.WriteMsg($"{Name} gemmer sig i sæt skæg i håbet om ingen hørte den");
        }
        void BashfulRan2()
        {
            Msg("JEG ELSKER SNEHVIDE!");
            Msg("Hvad er det dog jeg siger...");
            Controller.WriteMsg($"{Name} gemmer sig hurtigt it sit skæg");
        }
        void BashfulRan3()
        {
            Msg("Sådan noget skæg med lidt sirup er faktisk ikke dårligt");
        }
        #endregion

        #region Join
        void BashfulJoin1()
        {
            Msg("Her kommer jeg... Dump!");
            Controller.WriteMsg($"{Name} falder over sit eget skæg og rødmer straks");
        }
        void BashfulJoin2()
        {
            Msg("Hvem rykkede mig i skægget?");
        }
        void BashfulJoin3()
        {
            Msg("Nogen der kan hjælpe mig med denne fildt i mit skæg?");
        }
        #endregion

        #region Leave
        void BashfulLeave1()
        {
            Msg("Jeg må hellere få redt mit skæg");
        }
        void BashfulLeave2()
        {
            Msg("...");
            Controller.WriteMsg($"{Name} rødmer og løber væk");
        }
        void BashfulLeave3()
        {
            Msg("Det er umuligt at spise for alt det skæg. Jeg finder lige en hækkesaks");
        }
        #endregion

        #region Mono
        void BashfulMono1()
        {
            DwarfNames d = Controller.GetNewDwarf();
            Msg("Mon ikke det var på tide at blive trimmet...");
            Controller.WriteMsg($"{Name} får {d} til at trimme sit skæg");
            Controller.AddDwarf(d);
        }
        void BashfulMono2()
        {
            Msg("Vi må have lavet en politik om ikke at gøre andre dværge flove");
        }
        void BashfulMono3()
        {
            Msg("Hvis bare Snehvide var her, så ville de andre være søde mod mig");
        }
        #endregion

        #region Double
        void BashfulDouble1(DwarfNames dwarf)
        {
            Msg($"Hey {dwarf} ser lige her");
            Controller.WriteMsg($"{Name} trækker {dwarf} ind i sæt skæg");
            Controller.RemoveDwarf(dwarf);
        }
        void BashfulDouble2(DwarfNames dwarf)
        {
            Msg("Der må være en anden måde at håndtere denne flovhed på...");
            Msg($"Nogen idéer {dwarf}?");
            Controller.WriteMsg($"Både {Name} og {dwarf} lægger deres hoved i blød");
        }
        void BashfulDouble3(DwarfNames dwarf)
        {
            DwarfNames d = Controller.GetNewDwarf();
            Controller.WriteMsg($"{Name} begynder at præsentere noget magi...");
            Controller.WriteMsg($"Ud at skægget trækker {Name} et ben, men det sidder fast...");
            Msg($"Vil du give en hånd {dwarf}?");
            Controller.WriteMsg($"{dwarf} hjælper til og ud af skægget kommer... {d}???");
            Controller.AddDwarf(d);
        }
        #endregion
    }
}