namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Dumpe (Dopey)
        private void BuildDopey()
        {
            RandomReaction = new List<Action>() { };
            JoinReaction = new List<Action>() { };
            LeaveReaction = new List<Action>() { };
            MonoReaction = new List<Action>() { };
            DoubleReaction = new List<Action<DwarfNames>>() { };
        }

        #region Random
        void DopeyRan1()
        {
            Msg("Aha... JEG KAN TALE?! o_O");
            Controller.WriteMsg("Resten: HAN KAN TALE?!");
        }
        void DopeyRan2()
        {

        }
        void DopeyRan3()
        {

        }
        #endregion

        #region Join
        void DopeyJoin1()
        {
            
        }
        void DopeyJoin2()
        {

        }
        void DopeyJoin3()
        {

        }
        #endregion

        #region Leave
        void DopeyLeave1()
        {
            
        }
        void DopeyLeave2()
        {

        }
        void DopeyLeave3()
        {

        }
        #endregion

        #region Mono
        void DopeyMono1()
        {
            
        }
        void DopeyMono2()
        {

        }
        void DopeyMono3()
        {

        }
        #endregion

        #region Double
        void DopeyDouble1(DwarfNames dwarf)
        {
            
        }
        void DopeyDouble2(DwarfNames dwarf)
        {

        }
        void DopeyDouble3(DwarfNames dwarf)
        {

        }
        #endregion
    }
}