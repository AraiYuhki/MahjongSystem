namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 清一色
    /// </summary>
    public class PerfectFlush : IHands
    {
        public int Tier => 6;
        public string Name => "清一色";
        public bool HasCallPenalty => true;

        public HandType Type => HandType.PerfectFlush;

        public bool Judge(HandsData data)
        {
            // すべて同一の数牌で和了できる
            return data.IsWin && data.IsAllSameSuit;
        }
    }
}
