namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Gnavpot (Grumpy)
        private void BuildGrumpy()
        {
            RandomReaction = new List<Action>() { GrumpyRan1, GrumpyRan2, GrumpyRan3 };
            JoinReaction = new List<Action>() { GrumpyJoin1, GrumpyJoin2, GrumpyJoin3 };
            LeaveReaction = new List<Action>() { GrumpyLeave1, GrumpyLeave2, GrumpyLeave3 };
            MonoReaction = new List<Action>() { GrumpyMono1, GrumpyMono2, GrumpyMono3 };
            DoubleReaction = new List<Action<DwarfNames>>() { GrumpyDouble1, GrumpyDouble2, GrumpyDouble3 };
        }


        #region Random
        void GrumpyRan1()
        {
            Msg("Stille, der er noget der prøver at være sur!");
        }
        void GrumpyRan2()
        {
            Msg("Man kunne vel sende en pande flyevende, bare for at se, hvad der sker?");
        }
        void GrumpyRan3()
        {
            Msg("... hmph ...");
        }
        #endregion

        #region Join
        void GrumpyJoin1()
        {
            Msg("Om jeg magter, NEJ!");
        }
        void GrumpyJoin2()
        {
            DwarfNames d = (DwarfNames)Rng.Next(0, 7);
            Msg($"Det er kraftedme bare din skyld {d}!");
            Controller.AddDwarf(d);
        }
        void GrumpyJoin3()
        {
            DwarfNames d = Controller.GetNewDwarf();
            Msg($"Hvis jeg skal, så skal {d} også!");
            Controller.AddDwarf(d);
        }
        #endregion

        #region Leave
        void GrumpyLeave1()
        {
            Msg("Endelig kan jeg være alene!");
        }
        void GrumpyLeave2()
        {
            Msg("Gid jeg kunne sige, det har været en fornøjelse...");
        }
        void GrumpyLeave3()
        {
            Msg("Grumpy out, see ya ヽ(ಠ_ಠ)ノ");
        }
        #endregion

        #region Mono
        void GrumpyMono1()
        {
            Msg("Denne verden er for glad til mig.");
        }
        void GrumpyMono2()
        {
            Msg("Jeg håber ikke der noget, der kommer til skade.");
        }
        void GrumpyMono3()
        {
            DwarfNames d = Controller.GetNewDwarf();
            Controller.WriteMsg($"{Name} føler der er nogen der snydes af det gode selskab. Han henter derfor {d}");
            Msg($"Kom så her {d}");
            Controller.AddDwarf(d);
        }
        #endregion

        #region Double
        void GrumpyDouble1(DwarfNames dwarf)
        {
            Msg($"Jeg mangter ikke skændes med dig {dwarf}, gå med dig!");
            Controller.RemoveDwarf(dwarf);
        }
        void GrumpyDouble2(DwarfNames dwarf)
        {
            DwarfNames d = Controller.GetNewDwarf();
            Controller.WriteMsg($"Noget slår klik, {Name} begynder at råbe af {dwarf}");
            Msg($"Hvad bilder du dig ind {dwarf}?");
            Controller.WriteMsg($"{d} træder til for at stoppe {Name}");
            Controller.AddDwarf(d);
        }
        void GrumpyDouble3(DwarfNames dwarf)
        {
            Msg("Hvis bare snehvide var her...");
            Controller.WriteMsg($"{Name} dagdrømmer, {dwarf} er sikker på der må uvejr på vej med sådan et smil på {Name}s ansigt...");
        }
        #endregion
    }
}