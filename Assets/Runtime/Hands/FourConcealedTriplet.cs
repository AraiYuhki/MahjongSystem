using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 四暗刻
    /// </summary>
    public class FourConcealedTriplet : IHands
    {
        public int Tier => 13;
        public string Name => "四暗刻";
        public HandType Type => HandType.FourConcealedTriplets;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;

            // 鳴いてない刻子が4つあってかつ頭があれば成立
            return data.Elements.Count(element => element.IsTriplet && !element.IsCalled) == 4;
        }
    }
}
