namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Søvnig (Sleepy)
        private void BuildSleepy()
        {
            RandomReaction = new List<Action>() { SleepyRan1, SleepyRan2, SleepyRan3 };
            JoinReaction = new List<Action>() { SleepyJoin1, SleepyJoin2, SleepyJoin3 };
            LeaveReaction = new List<Action>() { SleepyLeave1, SleepyLeave2, SleepyLeave3 };
            MonoReaction = new List<Action>() { SleepyMono1, SleepyMono2, SleepyMono3 };
            DoubleReaction = new List<Action<DwarfNames>>() { SleepyDouble1, SleepyDouble2, SleepyDouble3 };
        }

        #region Random
        void SleepyRan1()
        {
            Msg("Om jeg snart skal sove, det kan I tro!");
        }
        void SleepyRan2()
        {
            Msg("zzzzzzzzz");
        }
        void SleepyRan3()
        {
            Msg("zzzzzzz... Jeg er vågen, jeg er vågen... zzzzzz");
        }
        #endregion

        #region Join
        void SleepyJoin1()
        {
            Msg("Hvad skal man da gøre få at få en god nats søvn");
        }
        void SleepyJoin2()
        {
            Msg("Der må altså være andre måder at vækkge mig på");
        }
        void SleepyJoin3()
        {
            Msg("Lige 5 minutter mere... zzzzz");
        }
        #endregion

        #region Leave
        void SleepyLeave1()
        {
            Msg("Endelig kan jeg få en på øjet");
        }
        void SleepyLeave2()
        {
            DwarfNames d = (DwarfNames)Rng.Next(0, 7);
            Controller.WriteMsg($"{Name} er for træt til at finde sin egen seng. Han falder derfor i søvn hos {d}");
            Controller.RemoveDwarf(d);
        }
        void SleepyLeave3()
        {
            Controller.WriteMsg($"{Name} ser ingen grund til at bevæge sig. Han tager en lur på stedet");
        }
        #endregion

        #region Mono
        void SleepyMono1()
        {
            DwarfNames d = Controller.GetNewDwarf();
            Controller.WriteMsg($"{Name} er meget træt og ser derfor dårligt. Han opfanger {d}s hvide trøje og tænker 'PUDE!'");
            Msg("Don't mind if I do... sikke en blød pude");
            Controller.AddDwarf(d);
        }
        void SleepyMono2()
        {
            Msg("Det må blive i morgen... zzz");
            Controller.RemoveDwarf(Name);
        }
        void SleepyMono3()
        {
            DwarfNames d = (DwarfNames)Rng.Next(0, 7);
            Controller.WriteMsg($"{Name} snakker i søvne");
            Msg("Simsalabim... flødeskum og kager... zzz");
            Controller.WriteMsg($"Alt det snakken forstyrre {d}");
            Controller.AddDwarf(d);
        }
        #endregion

        #region Double
        void SleepyDouble1(DwarfNames dwarf)
        {
            Msg("PUDEKAMP!");
            Controller.WriteMsg($"{Name} smadrer sin pude i hovedet af {dwarf}");
            Controller.WriteMsg($"{dwarf} er knocked out cold");
            Controller.RemoveDwarf(dwarf);
        }
        void SleepyDouble2(DwarfNames dwarf)
        {
            DwarfNames d = Controller.GetNewDwarf();
            Msg("Hvis jeg skal op, så skal I andre også!");
            Controller.WriteMsg($"{dwarf} er enig med {Name} og hjælper ham med at vække {d}");
            Controller.AddDwarf(d);
        }
        void SleepyDouble3(DwarfNames dwarf)
        {
            Controller.WriteMsg($"Efter gentagne forsøg har {Name} stadig svært ved at sove. Han beder derfor {dwarf} om en tjeneste");
            Msg($"Giv mig lige et BONK med dette bat vil du {dwarf}, det må altså hjælpe");
            Controller.WriteMsg($"{dwarf} forstår ikke helt pointen, men giver den alligevel hele armen. BONK!");
            Controller.RemoveDwarf(Name);
        }
        #endregion
    }
}