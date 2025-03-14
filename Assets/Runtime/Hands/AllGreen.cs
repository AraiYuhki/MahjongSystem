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
