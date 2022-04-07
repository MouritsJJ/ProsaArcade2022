namespace ProsaDwarfs.Dwarfs
{
    public static class Reactions
    {
        public static Dictionary<DwarfNames, List<Action>> JoinReaction = new Dictionary<DwarfNames, List<Action>>();
        public static Dictionary<DwarfNames, List<Action>> LeaveReaction = new Dictionary<DwarfNames, List<Action>>();
        public static Dictionary<DwarfNames, List<Func<bool>>> RandomReaction = new Dictionary<DwarfNames, List<Func<bool>>>();
        public static Dictionary<DwarfNames, List<Action>> MonoReaction = new Dictionary<DwarfNames, List<Action>>();
        public static Dictionary<DwarfNames, List<Action<IDwarf>>> DoubleReaction = new Dictionary<DwarfNames, List<Action<IDwarf>>>();
    }
}