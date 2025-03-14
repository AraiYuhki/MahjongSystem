using System.Collections.Generic;
using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 大三元
    /// </summary>
    public class BigDragons : IHands
    {
        public int Tier => 13;
        public string Name => "大三元";
        public HandType Type => HandType.BigDragons;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            var elements = data.Elements;
            return elements.Count(element => element.IsPairs && element.TileType.IsDragon()) == 3;
        }
    }
}
