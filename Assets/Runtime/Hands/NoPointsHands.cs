using System.Linq;

namespace Xeon.MahjongSystem
{

    /// <summary>
    /// 平和
    /// </summary>
    public class NoPointsHands : IHands
    {
        public int Tier => 1;
        public string Name => "平和";
        public HandType Type => HandType.NoPointsHands;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            // アガリ牌が字牌の場合は不成立
            if (data.WinningTile.IsHonours) return false;
            // 鳴いていたら不成立
            if (data.HasCalled) return false;

            // 順子が4つないと不成立
            if (data.ElementCounts[ElementsType.Sequence] != 4) return false;

            var pair = data.Elements.First(element => element.IsPair);
            // 役牌が雀頭だと不成立
            // 三元牌
            if (pair.TileType.IsDragon()) return false;
            // 自風・場風と同じか？
            if (pair.TileType == data.RoundWind || pair.TileType == data.SelfWind) return false;

            var lastTiles = data.WinningHands.TileDataList.ToList();
            lastTiles.Remove(data.WinningTile);

            // 残りの塔子に老頭牌が含まれている場合は条件を満たさないので不成立
            return !lastTiles.Any(tile => tile.IsTerminal);
        }
    }
}
