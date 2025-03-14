using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 小四喜
    /// </summary>
    public class LittleWinds : IHands
    {
        public int Tier => 13;
        public string Name => "小四喜";

        public HandType Type => HandType.LittleWinds;

        public bool Judge(HandsData data)
        {
            var elements = data.Elements;
            if (!data.IsNormalWin) return false;
            // 3つの刻子・槓子がすべて風牌である
            if (elements.Count(element => element.IsPairs && element.TileType.IsWind()) != 3) return false;
            // 雀頭は風牌である必要がある
            return data.HeadTileType.HasValue && data.HeadTileType.Value.IsWind();
        }
    }
}
