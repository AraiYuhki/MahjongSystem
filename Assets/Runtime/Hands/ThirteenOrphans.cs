using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 国士無双
    /// </summary>
    public class ThirteenOrphans : IHands
    {
        public int Tier => 13;
        public string Name => "国士無双";
        public HandType Type => HandType.ThirteenOrphans;
        public bool Judge(HandsData data)
        {
            return data.IsWinThirteenOrphans && !data.HasCalled;
        }
    }
}
