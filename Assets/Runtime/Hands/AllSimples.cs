using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 断么九
    /// </summary>
    public class AllSimples : IHands
    {
        public int Tier => 1;
        public string Name => "断么九";
        public HandType Type => HandType.AllSimples;
        public bool Judge(HandsData data)
        {
            if (!data.IsWin || data.HasHornors) return false;
            // 字牌と老頭牌が含まれている場合は不成立
            return !data.Tiles.Any(tile => tile.IsTerminal);
        }
    }
}
