using System.Collections.Generic;
using System.Linq;

namespace Xeon.MahjongSystem
{
    public class ReachData
    {
        private TileData discard;
        private List<TileData> readyTiles;

        public TileData Discard => discard;
        public List<TileData> ReadyTiles => readyTiles;

        public ReachData() { }
        public ReachData(TileData discard, List<TileData> readyTiles)
        {
            this.discard = discard;
            Initialize(readyTiles);
        }
        public ReachData(TileData discard, params TileData[] readyTiles)
        {
            this.discard = discard;
            Initialize(readyTiles);
        }
        public ReachData(int discardId, List<TileData> readyTiles)
        {
            discard = new TileData(discardId);
            Initialize(readyTiles);
        }

        private void Initialize(IEnumerable<TileData> readyTiles)
        {
            this.readyTiles = readyTiles.OrderBy(tile => tile.GetId()).ToList();
        }

        public override string ToString()
        {
            return $"打牌:{discard}, 聴牌:{string.Join(",", ReadyTiles)}";
        }

        public static bool operator ==(ReachData a, ReachData b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            if (a.discard != b.discard) return false;
            if (a.readyTiles.Count != b.readyTiles.Count) return false;
            for (var index = 0; index < a.readyTiles.Count; index++)
            {
                if (a.readyTiles[index] != b.readyTiles[index]) return false;
            }
            return true;
        }

        public static bool operator !=(ReachData a, ReachData b)
            => !(a == b);

        public override int GetHashCode()
            => $"{discard}:{string.Join(",", readyTiles)}".GetHashCode();
        public override bool Equals(object obj)
        {
            if (obj is not ReachData other) return false;
            return this == other;
        }
    }
}
