using System.Collections.Generic;
using System.Linq;
namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 混一色
    /// </summary>
    public class CommonFlush : IHands
    {
        public int Tier => 3;
        public string Name => "混一色";
        public bool HasCallPenalty => true;
        public HandType Type => HandType.CommonFlush;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            // 字一色の場合は成立しないので無視
            if (data.Tiles.All(tile => tile.IsHonours)) return false;

            var tileType = data.Tiles.First(tile => tile.IsSuits).Type;
            // 数牌がすべて同じ種類である
            // 字牌が含まれていないと清一色になる
            return data.Tiles.Where(tile => tile.IsSuits).All(tile => tileType == tile.Type)
                && data.Tiles.Any(tile => tile.IsHonours);
        }
    }
}
