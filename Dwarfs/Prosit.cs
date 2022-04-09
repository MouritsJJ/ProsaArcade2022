namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Prosit (Sneezy)
        private void BuildSneezy()
        {
            RandomReaction = new List<Action>() { SneezyRan1, SneezyRan2, SneezyRan3, SneezyRan4 };
            JoinReaction = new List<Action>() { SneezyJoin1, SneezyJoin2, SneezyJoin3 };
            LeaveReaction = new List<Action>() { SneezyLeave1, SneezyLeave2, SneezyLeave3 };
            MonoReaction = new List<Action>() { SneezyMono1, SneezyMono2, SneezyMono3 };
            DoubleReaction = new List<Action<DwarfNames>>() { SneezeDouble1, SneezeDouble2, SneezeDouble3 };
        }

        #region Random
        void SneezyRan1()
        {
            Msg("ACHUUUUUUUUUUUU!");
            Controller.WriteMsg($"{Name} nyser så hårdt, han flyver gennem rummet lige ind i klaveret og får en rød bagdel");
        }
        void SneezyRan2()
        {
            Msg("Snøft Snøft...");
        }
        void SneezyRan3()
        {
            DwarfNames d = Controller.GetNewDwarf();
            Msg("AH ... AAAHH ... AAAAAAHHHHHHHH");
            Controller.WriteMsg($"{d} skynder sig hen og sætter fingeren under {Name} næste");
            Msg($"Tak {d}! Der redede du mig vidst (づ｡◕‿‿◕｡)づ");
            Controller.AddDwarf(d);
        }
        void SneezyRan4()
        {
            Msg("ACHUU!");
            Controller.WriteMsg("Resten: CORONAAA!");
        }
        #endregion

        #region Join
        void SneezyJoin1()
        {
            Msg("Nogen der kender en modgift mod evig forkølelse?");
        }
        void SneezyJoin2()
        {
            Msg("Var der nogen, der kaldte?");
        }
        void SneezyJoin3()
        {
            Msg("Der må være sket en fejl, jeg nyser i... ACHU! Ligemeget");
        }
        #endregion

        #region Leave
        void SneezyLeave1()
        {
            Msg("ACHU!");
            Controller.WriteMsg($"{Name} nøs så hårdt hans hjerne straks lukkede ned");
        }
        void SneezyLeave2()
        {
            Msg("ACHU! Du milde moses, jeg tror det er bedst jeg går");
        }
        void SneezyLeave3()
        {
            Msg("Hvis der ikke var andet, så har jeg en næse at pudse");
        }
        #endregion

        #region Mono
        void SneezyMono1()
        {
            DwarfNames d = Controller.GetNewDwarf();
            Controller.WriteMsg($"{Name} nyser {d} lige i ansigtet");
            Controller.AddDwarf(d);
        }
        void SneezyMono2()
        {
            Controller.WriteMsg($"Snotten hænger langt ud af {Name}s næste");
            Controller.WriteMsg($"{Name} slikker sig om munden og går straks i panik over hvor ulækker denne situation er");
            Controller.RemoveDwarf(Name);
        }
        void SneezyMono3()
        {
            Msg("Det er som om, der er noget der lugter");
            Msg("ACHU! Ligemeget, jeg kan jo ikke lugte noget med denne næste alligevel");
        }
        #endregion

        #region Double
        void SneezeDouble1(DwarfNames dwarf)
        {
            Controller.WriteMsg($"{Name} snøfter så meget at {dwarf} kommer hen for at trøste ham");
            Msg($"Du skal ikke tænke på mig {dwarf}, jeg er bare snottet");
            Controller.WriteMsg($"Flov over sin antagelse skynder {dwarf} sig væk");
            Controller.RemoveDwarf(dwarf);
        }
        void SneezeDouble2(DwarfNames dwarf)
        {
            Msg($"Har du et lommetørklæde, {dwarf}?");
            Controller.WriteMsg($"{dwarf} rækker {Name} sit lommetørklæde");
            Msg($"ACHU! Tak for lån, vil du have det igen {dwarf}?");
            Controller.WriteMsg($"{dwarf} står helt forundret ¯\\(°_o)/¯");
        }
        void SneezeDouble3(DwarfNames dwarf)
        {
            Msg($"Hey {dwarf} vil du se noget fedt?");
            Controller.WriteMsg($"{Name} begynder at forme eifeltårnet i snot");
            Controller.WriteMsg($"Selv om {dwarf} er skræmt helt ind i sjælen vælger han at blive");
        }
        #endregion
    }
}