using ProsaDwarfs.Core;

namespace ProsaDwarfs.Dwarfs
{
    public partial class Dwarf
    {
        // Implementation of reactions for Søvnig (Sleepy)
        private void BuildSleepy()
        {
            RandomReaction = new List<Action>() { };
            JoinReaction = new List<Action>() { };
            LeaveReaction = new List<Action>() { };
            MonoReaction = new List<Action>() { };
            DoubleReaction = new List<Action<DwarfNames>>() { };
        }

        #region Random

        #endregion

        #region Join

        #endregion

        #region Leave

        #endregion

        #region Mono

        #endregion

        #region Double

        #endregion
    }
}