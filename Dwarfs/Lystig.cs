using ProsaDwarfs.Core;

namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Lystig (Happy)
        private void BuildHappy()
        {
            RandomReaction = new List<Action>() { HappyRan1, HappyRan2, HappyRan3 };
            JoinReaction = new List<Action>() { HappyJoin1, HappyJoin2, HappyJoin3 };
            LeaveReaction = new List<Action>() { HappyLeave1, HappyLeave2, HappyLeave3 };
            MonoReaction = new List<Action>() { HappyMono1, HappyMono2, HappyMono3 };
            DoubleReaction = new List<Action<DwarfNames>>() { HappyDouble1, HappyDouble2, HappyDouble3 };
        }

        #region Random
        void HappyRan1()
        {
            Msg("Lad os alle danse");
            Controller.WriteMsg($"{Name} begynder at danse, men ingen joiner ham");
        }
        void HappyRan2()
        {
            DwarfNames d = (DwarfNames)Rng.Next(0, 7);
            Msg($"Hvem vil have noget slik? {d}?");
            Controller.AddDwarf(d);
        }
        void HappyRan3()
        {
            Msg("Det kan ikke kun være mig, der synes dagen er fantastisk");
        }
        #endregion

        #region Join
        void HappyJoin1()
        {
            Msg("Yo, I'm back. Giv mig et smil :)");
        }
        void HappyJoin2()
        {
            Msg("Er livet ikke herligt mine dværgevenner?");
        }
        void HappyJoin3()
        {
            Msg("Så er det op på hesten igen. Afsted det gik.");
        }
        #endregion

        #region Leave
        void HappyLeave1()
        {
            Controller.WriteMsg($"{Name} har egentlig ikke lyst, men han må forlade de andre");
            Msg("Det er med vemodighed, jeg nu forlader jer mine dværge");
        }
        void HappyLeave2()
        {
            Msg("Neeej en pomfrit");
            Controller.WriteMsg($"{Name} indtager den sidste pomfrit uvidende om, det er deres sidste mad for vinteren");
            Controller.WriteMsg($"{Name} falder om, eftersom pomfritten har ligget længe og rådnet");
        }
        void HappyLeave3()
        {
            Controller.WriteMsg("Er det et fly? er det en måge? nej det er en skildpade");
            Controller.WriteMsg($"Og væk var {Name}");
        }
        #endregion

        #region Mono
        void HappyMono1()
        {
            DwarfNames d = Controller.GetNewDwarf();
            Controller.WriteMsg($"{Name} finder noget fyrværkeri i hjørnet");
            Msg($"{d} hjælp mig lige med at tænde disse til fejring af Snehvide");
        }
        void HappyMono2()
        {
            Controller.WriteMsg($"{Name} sidder underligt stille og kigger op:");
            Msg("Hvis bare der var en Snehvide til alle i verden, så ville verden være en bedre sted.");
        }
        void HappyMono3()
        {
            Msg("Humpty dumpty ( ͡° ͜ʖ ͡°)");
        }
        #endregion

        #region Double
        void HappyDouble1(DwarfNames dwarf)
        {
            Msg($"Må jeg se et smil {dwarf}?");
            Controller.WriteMsg($"{dwarf} finder ikke smilet frem");
            Msg("Nej okay, vi prøver igen næste år");
        }
        void HappyDouble2(DwarfNames dwarf)
        {
            Controller.WriteMsg($"{Name} ser en vaskebjørn udenfor");
            Msg($"{dwarf} kom med, vi skal ud og have os en ny ven");
            Controller.WriteMsg($"{Name} hiver {dwarf} ud af døren for at fodre vaskebjørnen");
            Controller.RemoveDwarf(Name);
            Controller.RemoveDwarf(dwarf);
        }
        void HappyDouble3(DwarfNames dwarf)
        {
            Controller.WriteMsg($"{Name} prøver at immitere {dwarf}");
            Msg($"Hey {dwarf} se lige mig");
            Controller.WriteMsg($"{dwarf} bliver så fornærmet man skulle tro han var Gnavpot selv");
            Controller.RemoveDwarf(dwarf);
        }
        #endregion
    }
}