namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 二盃口
    /// </summary>
    public class DoubleTwinSequences : IHands
    {
        public int Tier => 3;
        public string Name => "二盃口";
        public HandType Type => HandType.DoubleTwinSequences;
        public bool Judge(HandsData data)
        {
            return data.IsWinDoubleTwinSequences && !data.HasCalled;
        }
    }
}
