namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Dumpe (Dopey)
        private void BuildDopey()
        {
            RandomReaction = new List<Action>() { DopeyRan1, DopeyRan2, DopeyRan3 };
            JoinReaction = new List<Action>() { DopeyJoin1, DopeyJoin2, DopeyJoin3 };
            LeaveReaction = new List<Action>() { DopeyLeave1, DopeyLeave2, DopeyLeave3 };
            MonoReaction = new List<Action>() { DopeyMono1, DopeyMono2, DopeyMono3 };
            DoubleReaction = new List<Action<DwarfNames>>() { DopeyDouble1, DopeyDouble2, DopeyDouble3 };
        }

        #region Random
        void DopeyRan1()
        {
            Msg("Aha... JEG KAN TALE?! o_O");
            Controller.WriteMsg("Resten: HAN KAN TALE?!");
        }
        void DopeyRan2()
        {
            Controller.WriteMsg($"DONK DONK... {Name} falder over sit eget bukseben");
        }
        void DopeyRan3()
        {
            Msg("AV FOR SATAN!");
            Controller.WriteMsg($"{Name} smadrer sin lilletå ind i bordbenen");
            Msg("JEG KAN TALE?!");
            Controller.WriteMsg("Resten: HAN KAN TALE?!");
        }
        #endregion

        #region Join
        void DopeyJoin1()
        {
            Controller.WriteMsg($"Bonk Bonk... {Name} kommer trillende");
        }
        void DopeyJoin2()
        {
            Controller.WriteMsg($"Slaske slaske... {Name} kommer slæbende med sin alt for lange bukser.");
            Controller.WriteMsg($"Resten: {Name} falder i... BONK... Ligemeget, han falder");
        }
        void DopeyJoin3()
        {
            Controller.WriteMsg("BONK høres det straks");
        }
        #endregion

        #region Leave
        void DopeyLeave1()
        {
            Controller.WriteMsg($"DOING... En kæmpe fjeder sender {Name} ud");
        }
        void DopeyLeave2()
        {
            Controller.WriteMsg($"{Name} forsøger at løbe... BONK... {Name} falder");
            Controller.WriteMsg($"{Name} tænker til sig selv: Gulvet har også altid være et bedre sted til mit ansigt");
        }
        void DopeyLeave3()
        {
            DwarfNames d = (DwarfNames)Rng.Next(0, 7);
            Controller.WriteMsg($"{Name} er ved at falde, men får rettet op igen.");
            Controller.WriteMsg($"{Name} tror han har klaret det, men får væltet en armbolt ned over foden på {d}");
            Controller.WriteMsg($"Inden {d} når at sige et ord er han færdig med det hvide ud af øjnene");
            Controller.RemoveDwarf(d);
        }
        #endregion

        #region Mono
        void DopeyMono1()
        {
            Controller.WriteMsg($"{Name} prøver at sætte sig på en stol, men rammer ved siden af og slår røvet i gulvet.");
        }
        void DopeyMono2()
        {
            Controller.WriteMsg($"{Name} prøver at stå helt stille for at undgå sin klodsethed... BONK - {Name}s ansigt møder gulvet");
        }
        void DopeyMono3()
        {
            Controller.WriteMsg($"{Name} føler sig som en ny dværg...");
            Controller.WriteMsg("BONK! Det var han så ikke, må det konkluderes med sit ansigt mod døren");
        }
        #endregion

        #region Double
        void DopeyDouble1(DwarfNames dwarf)
        {
            Controller.WriteMsg($"{dwarf} hjælper {Name} op fra gulvet. Men {Name} falder straks igen");
        }
        void DopeyDouble2(DwarfNames dwarf)
        {
            DwarfNames d = Controller.GetNewDwarf();
            Controller.WriteMsg($"{dwarf}: Nogen der har set {Name}?...");
            Controller.WriteMsg($"{d} kigger herefter ned og opdager han står på {Name}");
            Controller.AddDwarf(d);
        }
        void DopeyDouble3(DwarfNames dwarf)
        {
            Controller.WriteMsg($"{Name} snupler og flyver 10m med hovedet først ind i maven på {dwarf}");
            Controller.WriteMsg($"{dwarf} tager slaget som en dværg og forsvinder straks");
            Controller.RemoveDwarf(dwarf);
        }
        #endregion
    }
}