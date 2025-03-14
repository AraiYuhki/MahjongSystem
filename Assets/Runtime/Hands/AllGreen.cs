using System.Collections.Generic;
using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 緑一色
    /// </summary>
    public class AllGreen : IHands
    {
        public int Tier => 13;
        public string Name => "緑一色";
        public HandType Type => HandType.AllGreen;
        private static readonly List<TileData> TargetTiles = new()
        {
            new TileData(TileType.GreenDragon),
            new TileData(TileType.Bamboos, 2),
            new TileData(TileType.Bamboos, 3),
            new TileData(TileType.Bamboos, 4),
            new TileData(TileType.Bamboos, 6),
            new TileData(TileType.Bamboos, 8),
        };

        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            foreach (var (id, count) in data.TileCounts)
            {
                if (count > 0 && !TileData.GreenIds.Contains(id))
                    return false;
            }
            return true;
        }
    }
}
