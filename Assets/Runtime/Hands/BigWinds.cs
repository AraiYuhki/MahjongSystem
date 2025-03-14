using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 大四喜
    /// </summary>
    public class BigWinds : IHands
    {
        public int Tier => 13;
        public string Name => "大四喜";
        public HandType Type => HandType.BigWinds;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            var elementCounts = data.ElementCounts;
            // 順子・塔子が存在しない
            if (elementCounts[ElementsType.Sequence] > 0 || elementCounts[ElementsType.SerialPair] > 0)
                return false;

            foreach (var element in data.Elements)
            {
                if (element.IsPair)
                {
                    if (element.TileType.IsWind())
                        return false;
                    continue;
                }
                if (!element.TileType.IsWind()) return false;
            }

            // 頭以外すべて風牌である必要がある
            return true;
        }
    }
}
