using System.Collections.Generic;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 三色同刻
    /// </summary>
    public class MixedTriplets : IHands
    {
        public int Tier => 2;
        public string Name => "三色同刻";
        public HandType Type => HandType.MixedTriplets;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            // 刻子が最低三つ以上必要
            if (data.ElementCounts[ElementsType.Triplet] < 3) return false;
            var tripletsCount = new Dictionary<int, int>();
            foreach (var element in data.Elements)
            {
                if (!element.IsTriplet || element.TileType.IsHonours()) continue;
                if (!tripletsCount.ContainsKey(element.Number))
                    tripletsCount[element.Number] = 0;
                tripletsCount[element.Number]++;

                if (tripletsCount[element.Number] == 3)
                    return true;
            }
            // 同じ数字の刻子が三つあれば成立
            return false;
        }
    }
}
