using System.Linq;

namespace Xeon.MahjongSystem
{

    /// <summary>
    /// 清老頭
    /// </summary>
    public class AllTerminals : IHands
    {
        public int Tier => 13;
        public string Name => "清老頭";
        public HandType Type => HandType.AllTerminals;
        public HandType[] NoCompsiteHands => new HandType[]
        {
        HandType.CommonEnds,
        HandType.PerfectEnds,
        HandType.CommonTerminals
        };
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            var elementsCount = data.ElementCounts;

            // 孤立牌・塔子・順子が存在する時点で不成立
            if (elementsCount[ElementsType.Sequence] > 0 || elementsCount[ElementsType.SerialPair] > 0) return false;
            // すべて老頭牌である必要がある
            return data.Tiles.All(tile => tile.IsTerminal);
        }
    }
}
