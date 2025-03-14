using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 対々和
    /// </summary>
    public class AllTriplets : IHands
    {
        public int Tier => 2;
        public string Name => "対々和";
        public HandType Type => HandType.AllTriplets;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            // すべて刻子・槓子で成立
            if (data.Elements.Any(element => element.IsSequence)) return false;
            // 刻子が最低一つ存在していないと成立しない
            return data.ElementCounts[ElementsType.Triplet] > 0;
        }
    }
}
