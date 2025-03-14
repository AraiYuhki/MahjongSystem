using System.Collections.Generic;

namespace Xeon.MahjongSystem
{
    public interface IPlayer
    {
        public List<TileData> HandTiles { get; }
        public IEnumerable<ElementsData> CallDataList { get; }
        public bool IsHost { get; }
        public bool IsReach { get; }
        public bool IsOneShot { get; }
        public int TurnCount { get; }
        public TileType SelfWind { get; }
    }
}
